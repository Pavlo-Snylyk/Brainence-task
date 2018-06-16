using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace WebApplication.TextRedactor
{
    public static class SentencesService
    {
        public static List<Sentences> GetSentences()
        {
            using (var dbContext = new SentencesContainer())
            {
                return dbContext.SentencesSet.AsEnumerable().ToList();
            }
        }

        public static Sentences GetSentence(Expression<System.Func<Sentences, bool>> predicate)
        {
            using (var dbContext = new SentencesContainer())
            {
                return dbContext.SentencesSet.Where(predicate).FirstOrDefault();
            }
        }

        public static void InsertSentence(Sentences sentence)
        {
            using (var dbContext = new SentencesContainer())
            {
                dbContext.Entry(sentence).State = EntityState.Added;
                dbContext.SaveChanges();
            }
        }

        public static void InsertSentences(List<Sentences> sentence)
        {
            using (var dbContext = new SentencesContainer())
            {
                dbContext.SentencesSet.AddRange(sentence);
                dbContext.SaveChanges();
            }
        }

        public static bool IfSentenceExist(Expression<System.Func<Sentences, bool>> predicate)
        {
            var IfSentenceExist = false;

            using (var dbContext = new SentencesContainer())
            {
                var sentense = dbContext.SentencesSet.Where(predicate).FirstOrDefault();

                if (sentense != null)
                    IfSentenceExist = true;

                return IfSentenceExist;
            }
        }

        public static void DeleteSentences()
        {
            using (var dbContext = new SentencesContainer())
            {
                var sentencesToDelete = GetSentences();

                foreach (var sentenceToDelete in sentencesToDelete)
                {
                    dbContext.SentencesSet.Attach(sentenceToDelete);
                    dbContext.Entry(sentenceToDelete).State = EntityState.Deleted;
                    dbContext.SaveChanges();
                }
                
                //dbContext.Entry(sentencesToDelete).State = EntityState.Deleted;
                //dbContext.SaveChanges();
                //dbContext.SentencesSet.RemoveRange(sentencesToDelete);
                //dbContext.SaveChanges();
            }
        }
    }
}