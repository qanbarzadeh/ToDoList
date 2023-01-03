# To-do List Management System

This project is the backend for a simple to-do list management system. It is implemented using a minimal RESTful API in C# and Asp.Net. The database used is Entity Framework with in-memory database, so the project can be started without a database server.

## Functionality

- Create a task - the user can create a new task and optionally set its due date
- Edit task - the user can edit the title and the due date of the task as well as marking it as completed
- List pending tasks - the user can see a list of tasks that are not past their due date and not marked as done.
- List overdue tasks - the user can see a list of tasks that are past their due date and not marked as done.

## Code Quality and Good Practices

- This porjects demonstrartes the use of clean architecture.

## Deployment 

-This project is published on Docker Hub. You can access the API documentation at [Docker Hub Repository](https://hub.docker.com/repository/docker/quantdevxx/todolist)
-To pull and run the image on your own system, run the following command:

## Pull and Run the Docker Image

```bash
docker pull quantdevxx/todolist
docker run -p 8080:80 -d quantdevxx/todolist 
```
The `docker pull` command will pull the image from Docker Hub,\n and the `docker run` command will run the image on port 8080 on your local machine.


-The API can then be accessed at http://localhost:8080/swagger/index.html.
