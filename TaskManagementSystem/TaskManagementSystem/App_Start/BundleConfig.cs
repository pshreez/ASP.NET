using System.Web;
using System.Web.Optimization;

namespace TaskManagementSystem
{
    public class BundleConfig
    {
        
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/AngularJS")
                .Include(
                        "~/Scripts/animate/angular.js",
                        "~/Scripts/animate/angular-animate.js",
                         "~/Scripts/angular-route.js",
                       "~/Scripts/AngularUI/ui-router.js",
                       "~/Scripts/angular-ui/ui-router-tabs.js",                                   
                         "~/Scripts/angular-ui/ui-bootstrap-tpls-0.14.3.min.js",                         
                       "~/Scripts/datetimepicker.js")

                          .Include("~/Scripts/AngularUI/angular-sanitize.js")
                          .Include("~/Scripts/Graph/Chart.min.js")
                            .Include("~/Scripts/Graph/angular-chart.js")
                             

                          .Include("~/Scripts/dropdown/lodash.min.js")
                           .Include("~/Scripts/AngularUI/angular-highlights.js")
                            .Include("~/Scripts/AngularUI/highlight.min.js")
                           .Include("~/Scripts/AngularUI/angularjs-dropdown-multiselect.js")


                         
                          .Include("~/Scripts/MyScripts/Controller/MenuController.js")

                          .Include("~/Scripts/MyScripts/Filters.js")
                           .Include("~/Scripts/MyScripts/Directives/ngDropdownMultiselect.js")

                         //login

                         .Include("~/Scripts/MyScripts/Services/LoginService.js")
                         .Include("~/Scripts/MyScripts/Controller/LoginController.js")
                         .Include("~/Scripts/MyScripts/Factories/LoginFactory.js")

                           //registration
                           .Include("~/Scripts/MyScripts/Factories/CheckUserFactory.js")
                         .Include("~/Scripts/MyScripts/Controller/RegisterController.js")
                         .Include("~/Scripts/MyScripts/Factories/RegisterFactory.js")
                          .Include("~/Scripts/MyScripts/Directives/ngUniqueDirective.js")

                          .Include("~/Scripts/MyScripts/Controller/UserDetailController.js")
                          .Include("~/Scripts/MyScripts/Controller/UserListController.js")
                         //create project
                         .Include("~/Scripts/MyScripts/Controller/ProjectCreateController.js")
                         .Include("~/Scripts/MyScripts/Factories/ProjectCreateFactory.js")
                          .Include("~/Scripts/MyScripts/Controller/ProjectListController.js")
                           .Include("~/Scripts/MyScripts/Controller/ProjectDetailController.js")
                            .Include("~/Scripts/MyScripts/Controller/MyProjectListController.js")
                            .Include("~/Scripts/MyScripts/Controller/MyProjectEditController.js")
                            .Include("~/Scripts/MyScripts/Controller/ProjectTasksListController.js")
                            .Include("~/Scripts/MyScripts/Controller/DeadlineProjectsController.js")


                         .Include("~/Scripts/MyScripts/Factories/TaskCreateFactory.js")
                         .Include("~/Scripts/MyScripts/Controller/TaskController.js")
                         .Include("~/Scripts/MyScripts/Controller/MyTaskListController.js")   //project manager
                         .Include("~/Scripts/MyScripts/Controller/MyTaskEditController.js")
                          .Include("~/Scripts/MyScripts/Controller/TaskListController.js")
                         .Include("~/Scripts/MyScripts/Controller/TaskDetailController.js")
                         .Include("~/Scripts/MyScripts/Factories/TaskAssignFactory.js")
                           .Include("~/Scripts/MyScripts/Controller/TaskAssignController.js")
                            .Include("~/Scripts/MyScripts/Controller/AssignedTaskList_dController.js") // developer
                            .Include("~/Scripts/MyScripts/Controller/UpdateTaskController.js")
                           .Include("~/Scripts/MyScripts/Controller/ModalInstanceCtrl.js")
                            .Include("~/Scripts/MyScripts/Controller/AssignTaskToUserController.js")
                            .Include("~/Scripts/MyScripts/Controller/DiscussionController.js")
                            

                         .Include("~/Scripts/MyScripts/Factories/aboutFactory.js")//.IncludeDirectory(ANGULAR_APP_ROOT, "*.js", searchSubdirectories: true);
                         .Include("~/Scripts/MyScripts/Factories/DataFactory.js")
                          .Include("~/Scripts/MyScripts/Controller/HomeMenuController.js")
                         .Include("~/Scripts/MyScripts/MyModule.js")
                         .Include("~/Scripts/MyScripts/Services/NotificationBarService.js"));
                         
                   //    )); 



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                      "~/Content/angular-chart.css")); 
        }
    }
}
