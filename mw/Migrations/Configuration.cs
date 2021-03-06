namespace mw.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;

    internal sealed class Configuration : DbMigrationsConfiguration<mw.Models.EntryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "mw.Models.EntryContext";
        }


        protected override void Seed(mw.Models.EntryContext context)
        {
            List<string> titles = new List<string>
            {
                "They don't call it a Quarter Pounder with Cheese?",
                "What do they call a Big Mac?",
                "Yolanda, I thought you said you were gonna be cool.",
                "What's better than coffee?",
                "A few thoughts for today",
                "This is just a tribute",
                "Titles don't actually exist, do they?"
            };

            List<string> paragraphs = new List<string>
            {
                "The path of the righteous man is beset on all sides by the iniquities of the selfish and the tyranny of evil men. Blessed is he who, in the name of charity and good will, shepherds the weak through the valley of darkness, for he is truly his brother's keeper and the finder of lost children. And I will strike down upon thee with great vengeance and furious anger those who would attempt to poison and destroy My brothers. And you will know My name is the Lord when I lay My vengeance upon thee.",
                "My money's in that office, right? If she start giving me some bullshit about it ain't there, and we got to go someplace else and get it, I'm gonna shoot you in the head then and there. Then I'm gonna shoot that bitch in the kneecaps, find out where my goddamn money is. She gonna tell me too. Hey, look at me when I'm talking to you, motherfucker. You listen: we go in there, and that nigga Winston or anybody else is in there, you the first motherfucker to get shot. You understand?",
                "You think water moves fast? You should see ice. It moves like it has a mind. Like it knows it killed the world once and got a taste for murder. After the avalanche, it took us a week to climb out. Now, I don't know exactly when we turned on each other, but I know that seven of us survived the slide... and only five made it out. Now we took an oath, that I'm breaking now. We said we'd say it was the snow that killed the other two, but it wasn't. Nature is lethal but it doesn't hold a candle to man.",
                "Well, the way they make shows is, they make one show. That show's called a pilot. Then they show that show to the people who make shows, and on the strength of that one show they decide if they're going to make more shows. Some pilots get picked and become television programs. Some don't, become nothing. She starred in one of the ones that became nothing.",
                "Do you see any Teletubbies in here? Do you see a slender plastic tag clipped to my shirt with my name printed on it? Do you see a little Asian child with a blank expression on his face sitting outside on a mechanical helicopter that shakes when you put quarters in it? No? Well, that's what you see at a toy store. And you must think you're in a toy store, because you're here shoping for an infant named Jeb."
            };

            Random r = new Random();
            StringBuilder body = new StringBuilder();
            for (int i = 0; i < 10; i++) {
                body.Append("<p>" + paragraphs[r.Next(paragraphs.Count)] + "</p>");
            }

            for (int i = 0; i < 150; i++) {
                context.Entries.Add(new Models.Entry
                {
                    Title = string.Format(titles[r.Next(titles.Count)]),
                    Body = string.Format(body.ToString())
                });
            }
        }
    }
}
