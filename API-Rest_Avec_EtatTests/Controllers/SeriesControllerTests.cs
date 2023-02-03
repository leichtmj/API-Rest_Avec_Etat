using Microsoft.VisualStudio.TestTools.UnitTesting;
using API_Rest_Avec_Etat.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Rest_Avec_Etat.Models.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace API_Rest_Avec_Etat.Controllers.Tests
{
    [TestClass()]
    public class SeriesControllerTests
    {
        public SeriesDbContext _context;
        SeriesController SeriesController;

        public SeriesControllerTests()
        {
            var builder = new DbContextOptionsBuilder<SeriesDbContext>().UseNpgsql("Server=localhost;port=5432;Database=SeriesDB; uid=postgres; password=postgres;");
            _context = new SeriesDbContext(builder.Options);
            SeriesController = new SeriesController(_context);
        }


        [TestMethod()]
        public void SeriesControllerTest()
        {
            DbSet<Serie> test = _context.Series;

            Assert.IsNotNull(test);
        }

        [TestMethod()]
        public void GetSeriesTest()
        {
            List<Serie> series = new List<Serie>();
            series.AddRange(new List<Serie> {
                new Serie(1 ,  "Scrubs" ,   "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !" , 9 ,  184, 2001  ,  "ABC (US)"),
                new Serie(2, "James May's 20th Century" , "The world in 1999 would have been unrecognisable to anyone from 1900. James May takes a look at some of the greatest developments of the 20th century, and reveals how they shaped the times we live in now.",  1 ,  6 ,  2007  ,  "BBC Two"),
                new Serie(3 ,"True Blood" ,   "Ayant trouvé un substitut pour se nourrir sans tuer (du sang synthétique), les vampires vivent désormais parmi les humains. Sookie, une serveuse capable de lire dans les esprits, tombe sous le charme de Bill, un mystérieux vampire. Une rencontre qui bouleverse la vie de la jeune femme..." , 7 ,  81 , 2008 ,   "HBO")
            });


            var getserie = SeriesController.GetSeries().Result;
            List<Serie> tolist = new List<Serie>();

            tolist = getserie.Value.Where(s => s.Serieid <= 3).ToList();
            CollectionAssert.AreEqual(series, tolist);
        }



        [TestMethod()]
        public void GetSerieTest()
        {
            Serie s = new Serie(1, "Scrubs", "J.D. est un jeune médecin qui débute sa carrière dans l'hôpital du Sacré-Coeur. Il vit avec son meilleur ami Turk, qui lui est chirurgien dans le même hôpital. Très vite, Turk tombe amoureux d'une infirmière Carla. Elliot entre dans la bande. C'est une étudiante en médecine quelque peu surprenante. Le service de médecine est dirigé par l'excentrique Docteur Cox alors que l'hôpital est géré par le diabolique Docteur Kelso. A cela viennent s'ajouter plein de personnages hors du commun : Todd le chirurgien obsédé, Ted l'avocat dépressif, le concierge qui trouve toujours un moyen d'embêter JD... Une belle galerie de personnage !", 9, 184, 2001, "ABC (US)");
            var res = SeriesController.GetSerie(s.Serieid).Result;
            ActionResult<Serie> toserie = res;
            Serie final = toserie.Value;

            Assert.AreEqual(s, final);

        }


        [TestMethod()]
        public void GetSerieTestQuiEchoue()
        {
            var res = SeriesController.GetSerie(-1).Result;
            ActionResult<Serie> toserie = res;
            Serie final = toserie.Value;

            Assert.IsNull(final);
            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult), "Pas une série"); // Test du type du contenu (valeur) du retour

        }

        [TestMethod()]
        public void PutSerieTest()
        {
            Serie s = new Serie(22, "Stargate SG-1 : Age of Jafar", "Un anneau de trois mètres de diamètre fait d’un métal inconnu sur Terre constitue en fait une porte ouvrant un passage vers d’autres planètes. Des équipes de militaires et de scientifiques explorent ces différentes planètes, cherchant de nouvelles technologies pour combattre notamment les Goa’ulds, une espèce extraterrestre qui parasite des humains. Parmi ces équipes d’exploration, SG-1 devient la plus réputée, avec le colonel O’Neill, l’égyptologue Jackson, le major Carter et Teal’c, un Jaffa venant de Chulak, un autre monde.", 10, 216, 1997, "Syfy");

            var res = SeriesController.PutSerie(s.Serieid, s);

            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult));

        }

        [TestMethod()]
        public void PutSerieTestBadRequest()
        {
            Serie s = new Serie(22, "Stargate SG-1 : Age of Jafar", "Un anneau de trois mètres de diamètre fait d’un métal inconnu sur Terre constitue en fait une porte ouvrant un passage vers d’autres planètes. Des équipes de militaires et de scientifiques explorent ces différentes planètes, cherchant de nouvelles technologies pour combattre notamment les Goa’ulds, une espèce extraterrestre qui parasite des humains. Parmi ces équipes d’exploration, SG-1 devient la plus réputée, avec le colonel O’Neill, l’égyptologue Jackson, le major Carter et Teal’c, un Jaffa venant de Chulak, un autre monde.", 10, 216, 1997, "Syfy");

            var res = SeriesController.PutSerie(23, s);

            Assert.IsInstanceOfType(res.Result, typeof(BadRequestResult));

        }


        [TestMethod()]
        public void PutSerieTestNotFound()
        {
            Serie s = new Serie(-1, "Stargate SG-1 : Age of Jafar", "Un anneau de trois mètres de diamètre fait d’un métal inconnu sur Terre constitue en fait une porte ouvrant un passage vers d’autres planètes. Des équipes de militaires et de scientifiques explorent ces différentes planètes, cherchant de nouvelles technologies pour combattre notamment les Goa’ulds, une espèce extraterrestre qui parasite des humains. Parmi ces équipes d’exploration, SG-1 devient la plus réputée, avec le colonel O’Neill, l’égyptologue Jackson, le major Carter et Teal’c, un Jaffa venant de Chulak, un autre monde.", 10, 216, 1997, "Syfy");

            var res = SeriesController.PutSerie(s.Serieid, s);

            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult));

        }

        [TestMethod()]
        [ExpectedException(typeof(System.AggregateException))]
        public void PutSerieTestViolationContrainteBase()
        {
            Serie s = new Serie(22, null, "Un anneau de trois mètres de diamètre fait d’un métal inconnu sur Terre constitue en fait une porte ouvrant un passage vers d’autres planètes. Des équipes de militaires et de scientifiques explorent ces différentes planètes, cherchant de nouvelles technologies pour combattre notamment les Goa’ulds, une espèce extraterrestre qui parasite des humains. Parmi ces équipes d’exploration, SG-1 devient la plus réputée, avec le colonel O’Neill, l’égyptologue Jackson, le major Carter et Teal’c, un Jaffa venant de Chulak, un autre monde.", 10, 216, 1997, "Syfy");

            _ = SeriesController.PutSerie(s.Serieid, s).Result;
            //Thread.Sleep(1000);

        }

        [TestMethod()]
        public void PostSerieTest()
        {
            Serie s = new Serie(1234, "Dora Rise : Sunbreak", "Dora collabore avec les plus grands héros du MCU tel que Calendar Man et Squirrel Girl", 4, 127, 2024, "Disney ++");

            var res = SeriesController.PostSerie(s).Result;
            Thread.Sleep(1000);


            Assert.IsInstanceOfType(res, typeof(ActionResult<Serie>), "Type retour OK"); // Test du type du contenu (valeur) du retour
            Assert.IsInstanceOfType(res.Result, typeof(CreatedAtActionResult), "Type retour OK"); // Test du type du contenu (valeur) du retour

            _ = SeriesController.DeleteSerie(s.Serieid);

        }


        [TestMethod()]
        public void DeleteSerieTest()
        {
            Serie s = new Serie(9999, "Dora Rise : Sunbreak", "Dora collabore avec les plus grands héros du MCU tel que Calendar Man et Squirrel Girl", 4, 127, 2024, "Disney ++");
            _ = SeriesController.PostSerie(s);
            Thread.Sleep(1000);
            var res = SeriesController.DeleteSerie(9999);

            Assert.IsInstanceOfType(res.Result, typeof(NoContentResult));

        }

        [TestMethod()]
        public void DeleteSerieTestNotFound()
        {
            var res = SeriesController.DeleteSerie(10000);

            Assert.IsInstanceOfType(res.Result, typeof(NotFoundResult));
        }



    }
}