using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorldOfZuul;

namespace WorldOfZuul
{
    public sealed class GlacialBiome : Biome
    {
        public override void WelcomeMessage() //in case we decided to use those
        {
            Console.WriteLine($"Hello, {GameManager.playerName}! Welcome to the Glacial Biome!");
            Console.WriteLine("Explore the map, find different items, try talking to people you meet and complete the quests while learning about\nglaciers and polar areas! Have fun!");
        }

        public GlacialBiome()
        {
            BiomeName = "Glacial Biome"; // can be changed to glacial, just needed a placeholder so the changed biome name display works

            GlacialBiome.PointsToWin = 100;
            GlacialBiome.BiomeType = Biomes.Glacial;

            //items
            Item letter = new Item("Letter", "It's addressed to someone at the Research Station", true);
            Item thermometer = new Item("Thermometer", "Alcohol thermometer that can be used to measure very low temperatures", true);
            Item toolbox = new Item("Toolbox", "Full of screwdrivers and other equipment. Must-have while repairing something.", true);
            Item message = new Item("Message", "It's an encrypted message. It says 'kmjozxo ziqdmjihzio!'", true);
            //kmjozxo ziqdmjihzio! is protect environment! encrypted with caesar cipher,5

            //npcs
            Researcher researcher = new Researcher("Researcher", "Man working at the Research Station.", letter, thermometer);
            Villager villager = new Villager("Villager", "Kind looking person who seems to be living here.");
            Scientist scientist = new Scientist("Scientist", "Woman working at the Research Station. She's holding some documents, noting something down.");
            Man oldMan = new Man("Man", "Old man standing in front of a building.");
            MysteriousPerson mysteriousPerson = new MysteriousPerson("???", "A mysterious person", message);
            Quizzer quizzer = new Quizzer("Quizzer", "Person who makes quizzes");
            Fisherman fisherman = new Fisherman("Fisherman", "Person fishing in the frozen lake");


            Room? glacialRoom1 = new("Snowy Village",
                "You’re in a small village. There are not many people here. Everyone is wearing warm clothes. There’s a lot of snow everywhere. It’s hard to walk. It’s very cold. On the west you can see ice desert – it’s just snow, no sign of life. It’s not a good idea to go there, you might get lost. You can see some buildings on the south.",
                new List<Item> { letter, new Map("Map", "Map of the Glacial Biome.") }, new List<NPC> { villager, oldMan });

            PuzzleRoom? glacialRoom2 = new("Ice Coast",
                "You’re at the coast. You can see some penguins a bit further away. On the east there is the Ice Island. But there’s no bridge that you could use to get there. On the west there is the village.",
                "You’re at the coast. You can see some penguins a bit further away. On the east there is the Ice Island. You could use the small boat you got from the old man to get there. On the west there is the village.",
                "Boat",
                new List<Item> { }, new List<NPC> { }, false, "east");

            Room? glacialRoom3 = new("Ice Lake",
                "On the west there is the Ice Lake. It’s completely frozen. There is someone nearby it, looks like they are fishing. On the south you can see a huge glacial mountain. It’s dangerous to go there without proper equipment.",
                new List<Item> { }, new List<NPC> { fisherman });

            Room? glacialRoom4 = new("Research Station",
                "You’re at the Research Station. There are some buildings and machines around. You can see a few people, they’re probably scientists working here.",
                new List<Item> { toolbox, new Item("Camera", "High quality camera used for research purposes by the scientists", true) },
                new List<NPC> { researcher, scientist });

            Room? glacialRoom5 = new("Glacial Cliff",
                "You’re standing on the glacial cliff. It's really slippery. On the east, few meters below, you can see the ocean.",
                new List<Item> { }, new List<NPC> { mysteriousPerson });

            PuzzleRoom? glacialRoom6 = new("Snowy Hillside",
                "You're at the Snowy Hillside. There might be some polar animals living around here. The top of the Glacier Mountain is on the south. It looks majestic, but very dangerous. It gives you the feeling that a snowslide might happen anytime. You can't go there without proper equipment.",
                "You're at the Snowy Hillside. There might be some polar animals living around here. The top of the Glacier Mountain is on the south. It looks majestic, but very dangerous. It gives you the feeling that a snowslide might happen anytime. You can use the equipment from the researcher to go there.",
                "Equipment",
                new List<Item> { }, new List<NPC> { }, false, "south");

            Room? glacialRoom7 = new("Snowy Foothills",
                "You’re at the bottom of the Glacier Mountain",
                new List<Item> { }, new List<NPC> { quizzer });

            Room? glacialRoom8 = new("Ice Valley",
                "You're in the Ice Valley. Everything around you is white - you’re surrounded by snow and ice. On the east there is a huge glacier mountain, it’s dangerous to go there without a proper equipment. On the south you can see a cave.",
                new List<Item> { }, new List<NPC> { });

            PuzzleRoom? glacialRoom9 = new("Ice Cave", "You've entered an Ice Cave. It's dark here, you can't really see anything.",
                "You're in the Ice Cave. There's ice all around you. You can see many icicles, ice-stalagmites, some ice columns and even one frozen waterfall.", "Flashlight",
                new List<Item> { new Item("Key", "It's probably used to unlock some doors", true) }, new List<NPC> { });

            Room? glacialRoom10 = new("Ice Island",
                "You're on the Ice Island, surrounded by water. It's very unstable, you shouldn't be here for too long.",
                new List<Item> { new Item("Flashlight", "It might be useful for exploring", true) }, new List<NPC> { });

            PuzzleRoom? glacialFinalRoom = new("The Glacier Mountain",
                "You're at the top of the Glacier Mountain. Right in front of you there is a small building with a satelite dish antenna on the top, but the door is locked.",
                "You're at the top of the Glacier Mountain. Right in front of you there is a small building with a satelite dish antenna on the top. Inside it, it looks like a communication centre. There are a lot of machines, a computer, fax machine and an electrical box.", "Key",
                new List<Item> { message }, new List<NPC> { });

            glacialRoom1.SetExits(null, glacialRoom2, glacialRoom4, null);
            glacialRoom2.SetExits(null, glacialRoom10, glacialRoom5, glacialRoom1);
            glacialRoom3.SetExits(null, glacialRoom4, glacialRoom7, null);
            glacialRoom4.SetExits(glacialRoom1, glacialRoom5, glacialRoom8, glacialRoom3);
            glacialRoom5.SetExits(glacialRoom2, null, null, glacialRoom4);
            glacialRoom6.SetExits(null, glacialRoom7, glacialFinalRoom, null);
            glacialRoom7.SetExits(glacialRoom3, glacialRoom8, null, glacialRoom6);
            glacialRoom8.SetExits(glacialRoom4, null, glacialRoom9, glacialRoom7);
            glacialRoom9.SetExits(glacialRoom8, null, null, null);
            glacialRoom10.SetExits(null, null, null, glacialRoom2);
            glacialFinalRoom.SetExits(glacialRoom6, null, null, null);

            startLocation = glacialRoom1;
            northmostRoom = glacialRoom1;
            GlacialBiome.rooms = new List<Room>(){ glacialRoom1, glacialRoom2, glacialRoom3, glacialRoom4,
            glacialRoom5, glacialRoom6, glacialRoom7, glacialRoom8, glacialRoom9, glacialRoom10, glacialFinalRoom};
            GameManager.Inventory = new Inventory();
        }

        public class Researcher : NPC
        {
            public Researcher(string name, string description, Item item1 = null, Item item2 = null) : base(name, description)
            {
                this.name = name;
                this.description = description;
                this.item1 = item1;
                this.item2 = item2;
            }
            protected string name;
            protected string description;
            protected Item item1;
            protected Item item2;

            // for quests
            protected bool foundLetter = false;
            protected bool isTheLetterQuestCompleted = false;
            protected bool talkWithResaercher1 = false;
            protected bool checkTemperature = false;
            protected bool talkWithResearcher2 = false;
            protected bool startTemperatureQuest = false;
            protected bool temperatureQuestCompleted = false;
            protected bool mountainQuestStarted = false;
            protected bool mountainQuestCompleted = false;
            protected bool askedAboutMessage = false;
            protected bool finishedQuests = false;

            bool isActiveItem(Item item)
            {
                Item? tempItem = GameManager.Inventory?.GetItem(item.name);
                if (tempItem != null && tempItem.isActive)
                    return true;
                return false;
            }
            bool isActiveItem(string activeItemName)
            {
                Item? tempItem = GameManager.Inventory?.GetItem(activeItemName);
                if (tempItem != null && tempItem.isActive)
                    return true;
                return false;
            }
            private void check()
            {
                if (foundLetter == false)
                {
                    Item? tempItem = GameManager.Inventory?.GetItem("Letter");
                    if (tempItem != null)
                        foundLetter = true;
                }
                if (temperatureQuestCompleted == false)
                    checkTemperature = isActiveItem(item2);
                if (mountainQuestCompleted == false)
                {
                    Item? tempItem = GameManager.Inventory?.GetItem("Screw");
                    if (tempItem != null)
                        mountainQuestCompleted = true;
                }
            }
            public override void Interact()
            {
                base.Interact();
                check();

                if (foundLetter == false)
                {
                    Console.WriteLine("Researcher: When is that letter going to come... Oh! Sorry, I'm a bit busy right now...");
                }
                else if (foundLetter == true && isTheLetterQuestCompleted == false)
                {
                    Console.WriteLine("\nResearcher: Oh! Hello there! You found the letter! I was supposed to receive it last week\nbut there was a snowstorm which apparently made the postman lose some of the letters.");
                    Console.WriteLine("\nResearcher: I was really worried because that letter contains something important for me…\nIt would be quite upsetting if it weren’t for you! Thank you!");
                    isTheLetterQuestCompleted = true;
                    GameManager.score += 5;
                    GameManager.Inventory.items.Remove(item1);
                }
                else if (isTheLetterQuestCompleted && talkWithResaercher1 == false)
                {
                    Console.WriteLine("\nResearcher: My name is Richard. I am a researcher here. \nMe and my team regularly make different measurements using specialized equipment to monitor the area.");
                    Console.WriteLine("It is also our job inform other people, especially governments, about the changes happening here. \nWe also try our best to take actions to slow down the glacial retreat. ");
                    Console.WriteLine("\nResearcher: You know, glaciers cover about 10% of Earth's land surface. And they have a very important");
                    Console.WriteLine("role in our climate – they stabilize it. \nMelting glaciers adds to rising sea levels, which increases coastal erosion \nand elevates storm surge as warming air and ocean temperatures create more frequent\nand intense coastal storms like hurricanes and typhoons. \nThe bad news is, the Arctic is changing faster than any other place on Earth , it’s warming twice as fast. \nThat’s why it’s important for us to stop global warming, which is the main cause of glaciers melting.");
                    talkWithResaercher1 = true;
                }
                else if (talkWithResaercher1 == true && checkTemperature == false)
                {
                    Console.WriteLine("\nResearcher: Could you do me a favor? It’s the time we make some check-ups, but I’m a bit busy here… \nPlease take the alcohol thermometer, which can measure very low temperatures, it should be around here somewhere. \nUsing it, examine different regions around. We need to regularly make sure if everything is alright \nand if there are no drastic changes in the temperature.");
                    if (startTemperatureQuest == false)
                    {
                        GameManager.currentPlayerRoom.AddItem(item2);
                        startTemperatureQuest = true;
                    }
                    else
                    {
                        Console.WriteLine("\nResearcher: If you need a hint - just use a thermometer in a place you want to measure the temperature at and come back to me.");
                    }
                }
                else if (checkTemperature == true && talkWithResearcher2 == false)
                {
                    Console.WriteLine("\nResearcher: You’re back? Great! Let me see the results…");
                    Console.WriteLine("...");
                    talkWithResearcher2 = true;
                    GameManager.score += 5;
                    Item? tempItem = GameManager.Inventory?.GetItem("Thermometer");
                    if (tempItem != null)
                        GameManager.Inventory.items.Remove(tempItem);
                    temperatureQuestCompleted = true;
                    Console.WriteLine("Researcher: The temperatures are getting higher and higher each year… That’s very problematic… \nIt might have very serious consequences on our planet… Unfortunately we cannot really stop it from happening. \nThat definitely doesn’t mean we should ignore it though! Even if we can’t stop it, we can slow it down! \nBut it requires many people working on it together! One person trying to reduce greenhouse gas \nemissions alone won’t have much effect, but if many of us do a little something, \nit’ll make a huge difference for sure! That’s why we should encourage others to maybe choose using a bike \nor walking, rather than driving a car. And definitely bring awareness on how important that actually is. \nFor example, burning fossil fuels releases large amounts of carbon dioxide into the air. \nIt’s a greenhouse gas, and that means it traps heat in our atmosphere, which causes global warming, \nthus melting glaciers as well. The average global temperature has already increased by 1 Celsius degree. \nThat may seem like no big deal, but it’s actually quite bad…");
                }
                else if (talkWithResearcher2 == true && mountainQuestStarted == false)
                {
                    Console.WriteLine("\nResearcher: To raise awarness about global warming, our team regularly take photos of the glaciers and then post them on the Internet.\nSometimes it's just photos, but sometimes there are also some research notes attatched or we post them as a timelapse.\nIt's the time we post something again, but it seems like there's no signal...\nDo you know where the Glacier Mountain is? On top of it there is a building with a dish anthenna on top.\nI assume there's something wrong with the electrical box. Could you go there and try to fix it?");
                    Console.WriteLine($"\nResearcher: Oh, right! {GameManager.playerName}, wait! You need a proper equipment to get there...\nI don't have it right now, because the Research Station's has broken yesterday... But you can try asking people from the village!");
                    mountainQuestStarted = true;
                }
                else if (isActiveItem("Message") && mountainQuestStarted == true && askedAboutMessage == false)
                {
                    Console.WriteLine("\nResearcher: Oh? You found a weird message? Let me see...\nIt's encrypted. I'm sorry, I can't help you with that. But I'm sure you can find someone who's looking for that message.");
                    askedAboutMessage = true;
                }
                else if (mountainQuestCompleted == true && finishedQuests == false)
                {
                    Console.WriteLine("\nResearcher: You managed to fix the electrical box? Wow! Thank you so much!\nNow we can post new photos on the Internet and educate people about global warming and glacial areas!");
                    GameManager.score += 10;
                    finishedQuests = true;
                }
                else
                {
                    Console.WriteLine("\nResearcher: What can we do to stop global warming...");
                }
            }
        }
        public class Villager : NPC
        {
            public Villager(string name, string description) : base(name, description)
            {
                this.name = name;
                this.description = description;
            }
            protected string name;
            protected string description;
            protected int conversationCounter = 0;
            public override void Interact()
            {
                switch (conversationCounter)
                {
                    case 0:
                        Console.WriteLine("\nVillager: Hello! You don't look familiar... What's your name?");
                        Console.WriteLine("\n...");
                        Console.WriteLine($"\nVillager: Nice to meet you, {GameManager.playerName}! It's a pleasure! I hope you like it here!\nRemember to wear warm clothes and don't be in the cold for too long! Feel free to talk to me again, if you wanna chat!\nI talk a lot though, don't I? Haha.");
                        conversationCounter++;
                        break;
                    case 1:
                        Console.WriteLine("\nVillager: Did you know that the Arctic is home for almost 4 milion people today? Isn't that impressive?\nI've read about it on the Research Station's website haha.");
                        GameManager.score++;
                        conversationCounter++;
                        break;
                    case 2:
                        Console.WriteLine("\nVillager: Have you already talked to the researcher already? I've heard he hasn't received his letter yet...");
                        GameManager.score++;
                        conversationCounter++;
                        break;
                    case 3:
                        Console.WriteLine("\nVillager: Do you want to go to the Glacier Mountain? If so, you'll need a proper equipment. Take this one.");
                        GameManager.Inventory.AddNPCItem(new Item("Equipment", "Proffessional equipment for climbing that you received from the researcher.", true));
                        conversationCounter++;
                        GameManager.score++;
                        break;
                    case 4:
                        Console.WriteLine("\nVillager: It's important to use green energy, because it doesn't produce so many greenhouse gasses and is renewable!\nIt's much healthier for the environment and may help slow down global warming!");
                        conversationCounter++;
                        GameManager.score++;
                        break;
                    default:
                        Console.WriteLine($"\nVillager: Hello, {GameManager.playerName}! Sorry, I'm out of things to talk about right now!");
                        GameManager.score += 5;
                        break;
                }
            }
        }
        public class Scientist : NPC
        {
            public Scientist(string name, string description) : base(name, description)
            {
                this.name = name;
                this.description = description;
            }
            protected string name;
            protected string description;
            protected bool hiddenLineDiscovered = false;
            protected int conversationCounter = 0;

            //Conversations topics 
            //1.Glaciers
            protected string[] glaciersConversation = new string[3] { "Glacier is a mass of ice formed from accumulated snow above the permanent snow line.",
                        "Glaciers have a very important role - keeping our climate stable.",
                        "Glaciers can be devided in three main groups: ice sheets/ice caps, mountain glaciers and piedmont glaciers/ice shelves."};
            protected int glaciersConversationCounter = 0;
            //2.Glacial regions
            protected string[] glacialRegionsConversation = new string[5] {"The Arctic is located at the northernmost part of our planet. It consists of the Arctic Ocean and parts of Canada, \nRussia, the USA, Greenland, Norway, Finland, Sweden and Iceland.",
                        "There is very little rain in Antarctica. The Antarctic dry valleys are among the driest places on Earth.\n Scientists believe no rain has fallen there for two million years.",
                        "Because of the Earth’s tilt, for at least one day a year there’s an entire day of darkness in the Arctic.",
                        "The most significant snow deposits are in Antarctica (on the south pole) and in Greenland (on the North pole).",
                        "Some people think that the Arctic is just cold, flat, white and unchanging area full of ice and snow.\nBut its landscape varies from place to place."};
            protected int glacialRegionsConversationCounter = 0;
            //3.People
            protected string[] peopleConversation = new string[4] { "Indigenous people of the Arctic are called ‘Inuits‘",
                        "Around 800 million people worldwide suffer from malnutrition, the majority of them live in South East Asia, followed by sub-Saharan Africa.",
                        "Inuit speak Inupiaq (Inupiatun), Inuinnaqtun, Inuktitut, Inuvialuktun, and Greenlandic languages",
                        "Native Alaskans have always relied on hunting and fishing for food rather than farming of the land.\nThis is because of the frozen ground and short growing season."};
            protected int peopleConversationCounter = 0;
            //4.SDGs
            protected string[] sdgsConversation = new string[7]{ "The Sustainable Development Goals are a universal call to action to end poverty, \nprotect the planet and improve the lives and prospects of everyone, everywhere. \nThey were adopted by all UN Member States in 2015.",
                        "The 17 SDGs are: 1.No poverty, 2.Zero hunger, 3.Good health and well-being, 4.Quality education, \n5.Gender equality, 6.Clean water and sanitation, 7.Affordable and clean energy, 8.Decent work and economic growth, \n9.Industry, innovation and infrastructure, 10.Reduced inequalities, 11.Sustainable cities and communities, \n12.Responsible consumption and production, 13.Climate action, 14.Life below water, 15. Life on land, \n16.Peace, justice, and strong institutions, and 17. Partnerships for the goals.",
                        "SDGs have 169 targets in total! Isn't that a lot?",
                        "SDG goal number 15 - Life on Land, is about protecting, restoring and promoting sustainable use of terrestrial ecosystems.",
                        "SDG 3: Good health and well-being aims is about ensuring a healthy life for all people and promote their well-being at all ages.",
                        "SDGs were adopted by United Nations in 2015.",
                        "The SDGs are scheduled to be achieved by 2030."};
            protected int sdgsConversationCounter = 0;
            //5.Animals
            protected string[] animalsConversation = new string[4] { "There are around 75 species of mammals in the Arctic. 16 of them live on or undeer the ice.",
                        "The animals native to the Arctic region include seals, walruses, the Arctic fox, white hares, reindeer and musk oxen. ",
                        "Arctic foxes are able to survive frigid temperatures as low as -50 Celsius degrees",
                        "The best-known example of invasive species in the Arctic is the red king crab"};
            protected int animalsConversationCounter = 0;
            //6.Random facts
            protected string[] randomConversation = new string[4] { "The word ‘Arctic’ comes from the Greek word for bear, Arktos.",
                        "Ciphers are systems for encrypting and decrypting data.",
                        "Sea ice is frozen seawater that floats on the ocean surface.",
                        "An upward looking sonar (ULS) is a sonar device pointed upwards, towards the surface of the sea. \nIt is used for example to measure sea ice depth and concentration."};
            protected int randomConversationCounter = 0;

            public override void Interact()
            {
                if (conversationCounter == 0)
                {
                    Console.WriteLine("\nScientist: Hello! Nice to meet you! My name is Ana, I'm a scientist. If you want to know more about glaciers,\nglacial areas or anything science related, just talk to me! I'll tell you everything I know!");
                    conversationCounter++;
                }
                else
                {
                    Console.WriteLine($"\nAna: Hello, {GameManager.playerName}! What would you like to talk about?");
                    Console.WriteLine("Choose a number:\n1.Glaciers\n2.Glacial regions\n3.People\n4.SDGs\n5.Animals\n6.Random facts\n7.Leave");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("\nAna: " + glaciersConversation[glaciersConversationCounter]);
                            glaciersConversationCounter++;
                            if (glaciersConversation.Length <= glaciersConversationCounter)
                                glaciersConversationCounter = 0;
                            break;
                        case 2:
                            Console.WriteLine("\nAna: " + glacialRegionsConversation[glacialRegionsConversationCounter]);
                            glacialRegionsConversationCounter++;
                            if (sdgsConversation.Length <= glacialRegionsConversationCounter)
                                glacialRegionsConversationCounter = 0;
                            break;
                        case 3:
                            Console.WriteLine("\nAna: " + peopleConversation[peopleConversationCounter]);
                            peopleConversationCounter++;
                            if (peopleConversation.Length <= peopleConversationCounter)
                                peopleConversationCounter = 0;
                            break;
                        case 4:
                            Console.WriteLine("\nAna: " + sdgsConversation[sdgsConversationCounter]);
                            sdgsConversationCounter++;
                            if (sdgsConversation.Length <= sdgsConversationCounter)
                                sdgsConversationCounter = 0;
                            break;
                        case 5:
                            Console.WriteLine("\nAna: " + animalsConversation[animalsConversationCounter]);
                            animalsConversationCounter++;
                            if (animalsConversation.Length <= animalsConversationCounter)
                                animalsConversationCounter = 0;
                            break;
                        case 6:
                            Console.WriteLine("\nAna: " + randomConversation[randomConversationCounter]);
                            randomConversationCounter++;
                            if (randomConversation.Length <= randomConversationCounter)
                                randomConversationCounter = 0;
                            break;
                        case 7:
                            Console.WriteLine("\nAna: See you!");
                            break;
                        case 17: //secret line, 17 becasue there's 17 sdg goals
                            Console.WriteLine("\nAna: Hey! That is our research station's code number...! You shouldn't know that...\nOkay listen! In case you're informed about certain stuff...\nWe use number 17 because there are 17 SDG goals... \nI guess for you to solve the puzzle the important things are: cipher, Caesar, five.\nIf you come across a weird message, you'll know what to do...\nHopefully");
                            if (hiddenLineDiscovered == false)
                            {
                                GameManager.score += 5;
                                hiddenLineDiscovered = true;
                            }
                            break;
                        default:
                            Console.WriteLine("\nAna: I don't know this topic, sorry...");
                            break;
                    }
                }
            }
        }
        public class Man : NPC
        {
            public Man(string name, string description) : base(name, description)
            {
                this.name = name;
                this.description = description;
            }
            protected string name;
            protected string description;
            protected int conversationCounter = 0;
            public override void Interact()
            {
                switch (conversationCounter)
                {
                    case 0:
                        Console.WriteLine("\nOld man: Oh? *cough* Who are you?");
                        conversationCounter++;
                        break;
                    case 1:
                        Console.WriteLine("\nOld man: Do you see that small boat over there? I used to use it a lot few years ago...\nEh... I'm too old for this now... *cough*\nYou should take it. Maybe it'll be more useful to you.");
                        GameManager.Inventory.AddNPCItem(new Item("Boat", "Small boat you received from an old man", true));
                        conversationCounter++;
                        break;
                    default:
                        Console.WriteLine("\nOld man: Back in my days...");
                        GameManager.score += 5;
                        break;
                }
            }
        }
        public class Fisherman : NPC
        {
            public Fisherman(string name, string description) : base(name, description)
            {
                this.name = name;
                this.description = description;
            }
            protected string name;
            protected string description;
            protected int conversationCounter = 0;
            public override void Interact()
            {
                switch (conversationCounter)
                {
                    case 0:
                        Console.WriteLine("\nFisherman: Hello there. If you're wondering - fishing here is allowed. But there are restrictions.\nAny fishery can only have a very limited impact on the ecosystem and should be managed in a way that minimalizes the risk of changing it.");
                        conversationCounter++;
                        GameManager.score++;
                        break;
                    case 1:
                        Console.WriteLine("\nFisherman: Fish in polar regions go deeper in water to stay warm as water retains its warmth under the icy layer.");
                        conversationCounter++;
                        GameManager.score++;
                        break;
                    case 2:
                        Console.WriteLine("\nFisherman: One of the most numerous fish species in the Arctic is the Arctic cod.");
                        conversationCounter++;
                        GameManager.score += 5;
                        break;
                    default:
                        Console.WriteLine($"Fisherman: We should somehow stop the invasive species...");
                        break;
                }
            }
        }
        public class Quizzer : NPC
        {
            public Quizzer(string name, string description) : base(name, description)
            {
                this.name = name;
                this.description = description;
            }
            protected string name;
            protected string description;
            protected int questionCounter = 0;
            protected string answer;
            public override void Interact()
            {
                switch (questionCounter)
                {
                    case 0:
                        Console.WriteLine("\nQuizzer: Hi! Let me introduce myself! My name is Ruka, but people here call me a Quizzer.\nI have some nice questions prepared for you, so come to me when you want to try asnwering them!\nThe rules are simple: one question at the time and you cannot skip the question. \nThat means if you don't know the answer... you're stuck.\nI hope that won't happen, but oh well...who knows! That depends only on you! Good luck!");
                        questionCounter++;
                        GameManager.score++;
                        break;
                    case 1:
                        Console.WriteLine("\nQuizzer: Hello! Glad to see you want to take part in the quizz! Here is the first question:\nHow many SDGs are there?");
                        answer = Console.ReadLine().ToLower();
                        if (answer == "17")
                        {
                            Console.WriteLine("\nQuizzer: Correct! Congratulaions! Talk to me again when you want to answer the next question!");
                            GameManager.score += 5;
                            questionCounter++;
                        }
                        else
                        {
                            Console.WriteLine("\nQuizzer: Wrong! Let's talk again when you know the answer...");
                        }
                        break;
                    case 2:
                        Console.WriteLine("\nQuizzer: Hello! Glad to see you want to take part in the quizz! Here is the next question:");
                        Console.WriteLine("What does 'SDGs' stand for?\n1.Smart Development Goals\n2.Sustainable Developing Goals\n3.Strong Developing Goals\n4.Sustainable Development Goals\nChoose a number.");
                        answer = Console.ReadLine().ToLower();
                        if (answer == "4")
                        {
                            Console.WriteLine("\nQuizzer: Correct! Congratulaions! Talk to me again when you want to answer the next question!");
                            GameManager.score += 5;
                            questionCounter++;
                        }
                        else
                        {
                            Console.WriteLine("\nQuizzer: Wrong! Let's talk again when you know the answer...");
                        }
                        break;
                    case 3:
                        Console.WriteLine("\nQuizzer: Hello! Glad to see you want to take part in the quizz! Here is the next question:");
                        Console.WriteLine("How many targets do SDGs have?");
                        answer = Console.ReadLine().ToLower();
                        if (answer == "169")
                        {
                            Console.WriteLine("\nQuizzer: Correct! Congratulaions! Talk to me again when you want to answer the next question!");
                            GameManager.score += 5;
                            questionCounter++;
                        }
                        else
                        {
                            Console.WriteLine("\nQuizzer: Wrong! Let's talk again when you know the answer...");
                        }
                        break;
                    case 4:
                        Console.WriteLine("\nQuizzer: Hello! Glad to see you want to take part in the quizz! Here is the next question:");
                        Console.WriteLine("In which year did the work on SDGs expired?");
                        answer = Console.ReadLine().ToLower();
                        if (answer == "2015")
                        {
                            Console.WriteLine("\nQuizzer: Correct! Congratulaions! Talk to me again when you want to answer the next question!");
                            GameManager.score += 5;
                            questionCounter++;
                        }
                        else
                        {
                            Console.WriteLine("\nQuizzer: Wrong! Let's talk again when you know the answer...");
                        }
                        break;
                    case 5:
                        Console.WriteLine("\nQuizzer: Hello! Glad to see you want to take part in the quizz! Here is the next question:");
                        Console.WriteLine("Who adopted the SDGs?\n1.United Nations\n2.European Union\n3.World Health Organisation\n4.North Atlantic Treaty Organization\nChoose a number.");
                        answer = Console.ReadLine().ToLower();
                        if (answer == "1")
                        {
                            Console.WriteLine("\nQuizzer: Correct! Congratulaions! Talk to me again when you want to answer the next question!");
                            GameManager.score += 5;
                            questionCounter++;
                        }
                        else
                        {
                            Console.WriteLine("\nQuizzer: Wrong! Let's talk again when you know the answer...");
                        }
                        break;
                    case 6:
                        Console.WriteLine("\nQuizzer: Hello! Glad to see you want to take part in the quizz! Here is the next question:");
                        Console.WriteLine("In which year are SDGs supposed to be achieved?");
                        answer = Console.ReadLine().ToLower();
                        if (answer == "2030")
                        {
                            Console.WriteLine("\nQuizzer: Correct! Congratulaions! Talk to me again when you want to answer the next question!");
                            GameManager.score += 5;
                            questionCounter++;
                        }
                        else
                        {
                            Console.WriteLine("\nQuizzer: Wrong! Let's talk again when you know the answer...");
                        }
                        break;
                    default:
                        Console.WriteLine($"Quizzer: Sorry, I'm out of quizzes at the moment...");
                        break;
                }
            }
        }
        public class MysteriousPerson : NPC
        {
            protected string name;
            protected string description;
            protected Item item1;

            public MysteriousPerson(string name, string description, Item item1) : base(name, description)
            {
                this.name = name;
                this.description = description;
                this.item1 = item1;
            }
            bool isActiveItem(Item item)
            {
                Item? tempItem = GameManager.Inventory?.GetItem(item.name);
                if (tempItem != null && tempItem.isActive)
                    return true;
                return false;
            }
            public override void Interact()
            {
                if (isActiveItem(item1))
                {
                    Console.WriteLine("\nMysterious Person: Hey! You found the message! Did you manage to decode it? Please tell me what it says!");
                    string answer = Console.ReadLine().ToString();
                    answer = answer.ToLower();
                    if (answer == "protect environment" || answer == "protect environment!")
                    {
                        Console.WriteLine("\nMysterious Person: Wow! I'm impressed! Good job!");
                        Console.WriteLine("I hope now you know a bit more about SDGs and how important glaciers are for the environment.\nCongratulations!\nWould you like to keep playing the game until you collect all the points or would you like to finish the Glacier Biome now and go to the next one?");
                        Console.WriteLine("Choose a number:\n1.Continue exploring\n2.Finish the Glacial Biome and move on");
                        bool choiceMade = false;
                        while (choiceMade == false)
                        {
                            int choice = Convert.ToInt32((Console.ReadLine()));
                            if (choice == 1)
                            {
                                Console.WriteLine("\nMysterious Person: Very well then! Come back here when you want to finish!");
                                choiceMade = true;
                            }
                            else if (choice == 2)
                            {
                                Console.WriteLine($"\nMysterious Person: Thank you for playing in the Glacial Biome, {GameManager.playerName}!");
                                GameManager.score += 100;
                                if (GameManager.score >= PointsToWin)
                                {
                                    Console.WriteLine("You have won! Congratulations!");
                                    Console.WriteLine("Score: " + GameManager.score);
                                    GameManager.glacialFinished = true;
                                    Program.Main();
                                }
                                choiceMade = true;
                            }
                            else
                            {
                                Console.WriteLine("\nMysterious Person: I think you said something wrong. Please try again.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nMysterios Person: No, that answer is incorrect.\nIf you need help, here's a small hint: when you look closely, you can see a number 17 written on the back.\nMaybe you should talk to someone who likes numbers a lot.");
                    }
                }
                else
                {
                    Console.WriteLine("\nMysterious Person: I don't have time to talk right now...");
                }
            }
        }
        public class PuzzleRoom : Room
        {
            bool usedItem = false;
            string neededItemName = "";
            public string ShortDescription { get; protected set; }
            string lockedLongDescription;
            string unlockedLongDescription;
            string lockedExit;
            bool hidingItems = false;
            bool fixedBox = false;

            private void checkingUsingItem()
            {
                Item? tempItem = GameManager.Inventory?.GetItem(neededItemName);
                if (tempItem == null || !tempItem.isActive)
                {
                    usedItem = false;
                }
                if (tempItem != null && tempItem.isActive)
                {
                    usedItem = true;
                }
            }
            private void finalRoomCheck()
            {
                Item? tempItem = GameManager.Inventory?.GetItem("Toolbox");
                if (tempItem != null && tempItem.isActive)
                {
                    GameManager.Inventory.items.Remove(tempItem);
                    GameManager.Inventory.AddNPCItem(new Item("Screw", "Old used up screw which was the reason the electrical box wasn't working correctly.", true));
                    fixedBox = true;
                }
            }
            public PuzzleRoom(string shortDesc, string longDesc, string longDescUnlocked, string itemName, List<Item> items, List<NPC> npcsInRoom, bool hideItems = true, string lockedExitName = "") : base(shortDesc, longDesc, items, npcsInRoom)
            {
                checkingUsingItem();
                Update();
                ShortDescription = shortDesc;
                lockedLongDescription = longDesc;
                unlockedLongDescription = longDescUnlocked;
                neededItemName = itemName;
                hidingItems = hideItems;
                lockedExit = lockedExitName;
                npcsInRoom = npcsInRoom ?? new List<NPC>();
            }
            public override void Update()
            {
                base.Update();
                checkingUsingItem();

                if (!usedItem)
                {
                    base.LongDescription = lockedLongDescription;
                    if (hidingItems)
                        base.HideItems();
                    if (lockedExit != null && lockedExit != "")
                        base.BlockExit(lockedExit);
                }
                else
                {
                    base.LongDescription = unlockedLongDescription;
                    if (hidingItems)
                        base.UnhideItems();
                    if (lockedExit != null && lockedExit != "")
                        base.UnlockExit(lockedExit);
                    if (ShortDescription == "The Glacier Mountain" && fixedBox == false)
                        finalRoomCheck();
                }
            }
        }
    }
}