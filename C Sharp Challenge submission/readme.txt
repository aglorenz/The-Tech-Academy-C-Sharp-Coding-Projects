For the C# Server Time challenge project (named "Challenge Submission" in the LMS), you create a new project with the template named "Web Application", which has the term "ASP.NET Core Web App" in the description of the template. Then, you modify the "Index.cshtml" file to something like this:
@page
@model IndexModel
@{
    ViewData["Title"] = "Home page";
}

<div class="text-center">
    <h1 class="display-4">Hello all, and welcome!</h1>

    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core%22%3Ebuilding Web apps with ASP.NET Core</a>.</p>
</div>

<html>
    <body>

    @{
    var greeting = "Welcome to my site! \n";
    var weekDay = DateTime.Now;
    var greetingMessage = greeting + "Here in Portland, OR it is: " + weekDay;
    }
    <p>@greetingMessage</p>

    </body>
</html>