using System.Web.Optimization;

namespace PsikoterapsitlerBurada
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                         "~/Scripts/underscore-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/datatables/jquery.dataTables.js",
                        "~/Scripts/datatables/dataTables.bootstrap.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/bootbox.min.js",
                        "~/Scripts/app/services/IndexService.js",
                        "~/Scripts/app/services/FavoriteQuestionService.js",
                        "~/Scripts/app/services/GetAnswersService.js",
                        "~/Scripts/app/services/VoteService.js",
                        "~/Scripts/app/services/LikeService.js",
                        "~/Scripts/app/services/WriteAnswerService.js",
                        "~/Scripts/app/services/SelectedUserService.js",
                        "~/Scripts/app/services/NotificationService.js",
                        "~/Scripts/app/services/SearchService.js",
                        "~/Scripts/app/controllers/SearchController.js",
                        "~/Scripts/app/controllers/WriteAnswerController.js",
                        "~/Scripts/app/controllers/NotificationController.js",
                        "~/Scripts/app/controllers/GetAnswersController.js",
                        "~/Scripts/app/controllers/IndexController.js",
                        "~/Scripts/app/controllers/SelectUserToAskQuestionController.js",
                        "~/Scripts/app/app.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/jquery.dataTables.css",
                        "~/Content/bootstrap.css",
                        "~/Content/site.css",
                        "~/Content/animate.css",
                        "~/Content/font-awesome-4.7.0/css/font-awesome.min.css"
                      ));
        }
    }
}
