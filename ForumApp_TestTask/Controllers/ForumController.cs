using ForumApp_TestTask.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using static ForumApp_TestTask.Controllers.ManageController;
using Microsoft.AspNet.Identity;

namespace ForumApp_TestTask.Controllers
{
    public class ArticleWithAuthor
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public int Id { get; set; }
        public DateTime ArticleDate { get; set; }
    };

    [Authorize]
    public class ForumController : Controller
    {
        readonly ApplicationDbContext db = new ApplicationDbContext();

        public ForumController()
        {
        }

        public ActionResult Index()
        {
            var topics = db.Topics;
            if (topics != null)
            {
                ViewBag.Topics = topics;
            }

            return View(); ;
        }

        public ActionResult Topic(int? id)
        {
            ViewBag.Topic = db.Topics.FirstOrDefault(x => x.Id == id);
            if (ViewBag.Topic == null)
            {
                return Redirect("/Forum");
            }

            ViewBag.Articles = null;

            var articles = from a in db.Articles.Where(a => a.TopicId == id)
                           join u in db.Users on a.UserId equals u.Id
                           select new ArticleWithAuthor
                           {
                               Content = a.Content,
                               Author = u.UserName,
                               Id = a.Id,
                               ArticleDate = a.UpdatedAt
                           };

            ViewBag.Articles = articles;

            return View();
        }

        [HttpGet]
        public ActionResult AddTopic()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTopic(string Title)
        {
            string curentUserId = User.Identity.GetUserId();
            Topic newTopic = db.Topics.FirstOrDefault(x => x.Title == Title);

            if (newTopic != null || Title == "")
            {
                return RedirectToAction("AddTopic");
            }

            Topic topic = new Topic
            {
                CreatedAt = DateTime.Now,
                Title = Title,
                UserId = curentUserId,
            };

            db.Topics.Add(topic);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddArticle(int? topicID)
        {
            if (topicID == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Topic = db.Topics.FirstOrDefault(x => x.Id == topicID);
            return View();
        }

        [HttpPost]
        public ActionResult AddArticle(string Content, int topicId)
        {
            if (Content == "")
            {
                return RedirectToAction("AddArticle", new { topicID = topicId });
            }

            string curentUserId = User.Identity.GetUserId();

            Topic topic = db.Topics.FirstOrDefault(x => x.Id == topicId);

            if (topic == null)
            {
                return Redirect("Forum");
            }

            Article article = new Article
            {
                Content = Content,
                TopicId = topicId,
                UserId = curentUserId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            db.Articles.Add(article);
            db.SaveChanges();

            return RedirectToAction("Topic", new { id = topicId });
        }

        [HttpGet]
        public ActionResult EditArticle(int? topicID, int? articleID)
        {
            Article article = db.Articles.FirstOrDefault(el => el.Id == articleID);
            Topic topic = db.Topics.FirstOrDefault(el => el.Id == topicID);

            if (article == null || topic == null || topicID == null)
            {
                return RedirectToAction("Topic", new { id = topicID });
            }
            ViewBag.topic = topic;

            string authorId = db.Users.Find(article.UserId).Id;
            string curentUserId = User.Identity.GetUserId();

            if (authorId == curentUserId)
            {
                ViewBag.article = article;
            }
            else
            {
                ViewBag.editArticleError = "Only the author can edit the article!";
            }

            return View();
        }

        [HttpPost]
        public ActionResult EditArticle(int? articleID, string Content)
        {
            Article article = db.Articles.FirstOrDefault(el => el.Id == articleID);
            if (article.Content != Content)
            {
                article.Content = Content;
                article.UpdatedAt = DateTime.Now;

                db.SaveChanges();
            }

            return RedirectToAction("Topic", new { id = article.TopicId });
        }
    }
}