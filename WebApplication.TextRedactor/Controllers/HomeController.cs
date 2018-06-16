using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Vereyon.Web;
using WebApplication.TextRedactor.Models;

namespace WebApplication.TextRedactor.Controllers
{
    public class HomeController : Controller
    {
        public static string IndexView = "Index";

        public ActionResult Index()
        {
            var model = new MainModel
            {
                SavedSentences = SentencesService.GetSentences()
            };

            return View(model);
        }
         
        [HttpPost]
        public ActionResult Save(MainModel mainModel)
        {
            var sentencesToInsert = new List<Sentences>();

            string allText = new StreamReader(mainModel.File.InputStream).ReadToEnd();
            string[] allSentences = allText.Split(new Char[] { '.' });

            foreach (var sentence in allSentences)
            {
                if (sentence.Contains(mainModel.InputText))
                {
                    var reversedSentence = Reverse(sentence);
                    var ifSentenceFromDbExist = SentencesService.IfSentenceExist(s => s.Text == reversedSentence && s.SearchWord == mainModel.InputText);

                    if (!ifSentenceFromDbExist)
                    {
                        var sentenceToInsert = FillParameterForInsert(reversedSentence, DateTime.Now, "Pavlo", mainModel.InputText);
                        sentencesToInsert.Add(sentenceToInsert);
                    }
                }
            }

            if (sentencesToInsert.Count != 0)
            {
                SentencesService.InsertSentences(sentencesToInsert);
                TempData["Message"] = "New sentences successfully inserted";
            }

            else
                TempData["Message"] = "Noting to insert";

            mainModel.SavedSentences = SentencesService.GetSentences();

            return View(IndexView, mainModel);
        }

        public ActionResult DeleteAllRecords() 
        {
            SentencesService.DeleteSentences();

            var model = new MainModel
            {
                SavedSentences = SentencesService.GetSentences()
            };

            TempData["Message"] = "Sentences successfully deleted from Db";
            return View(IndexView, model);
        }

        #region Private Methods

        private static string Reverse(string sentence)
        {
            char[] charArray = sentence.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private static Sentences FillParameterForInsert(string sentence, DateTime createdAt, string createdBy, string seachWord)
        {
            var newSentence = new Sentences
            {
                Text = sentence,
                CreatedAt = createdAt,
                CreatedBy = createdBy,
                SearchWord = seachWord
            };

            return newSentence;
        }

        #endregion
    }
}
