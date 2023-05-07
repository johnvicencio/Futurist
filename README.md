# Futurist
Futurist is a C#-based ASP.NET Core Blazor WebAssembly web app that generates random answers to personal questions about the future. The app's database is a JSON file that stores all possible answers, and the selected answer is displayed on the browser through a logic.

## Problem
A web app using web assembly will be created to allow users to ask questions and receive answers. Upon opening the app in a browser, a user can input a question and an answer is generated based on the question. To store all possible answers, a database will be utilized, ranging from a basic database to a more sophisticated relational database system like MySQL. In summary, the web app utilizes web assembly and a database to provide users with answers to their questions.

## Solution
Futurist is a Blazor WebAssembly app that utilizes ASP.NET Core and C#. The app has a Model class and a Services class that acts as a controller for the model. The Services class uses a method to return a random answer from a JSON file that acts as a database. On the presentation layer, there is a Razor component with an input box for the user to ask a question and receive an answer in return.

## Creating the app
1. Create a C# Blazor Webassembly app using Visual Studio.
2. Name the application Futurist.
3. Create a folder in the Shared folder: Prediction class inside a Model folder and PredictionService inside a Service folder.
4. Make sure that the PredictionService controller handles the randomized retrieval of an answer from the JSON file
6. Create an input box and displays a random answer 

## Technologies Used
1. ASP.NET Core
2. C#
3. Blazor Webassembly (with JavaScript)
4. Bootstrap with Materialize design
5. Content saved on GitHub
6. Deployed on Netifly
7. Added ChaptGPT for god mode check-box

## Using the application
1. Go to the URL: https://futurist.mythoslife.com 
2. Type an answer (input)
3. Wait for the answer below it (output)