# MarsRoverChallenge

## To Review API 

You need .netcore 3.1 Installed for the API
The Project can be reviewd using visual studio IDE
You can run tests using test explore in visual studio.

### How to execute API?
You can execute project from the VS IDE
A swagger interface will loaded to allow the user to test the endpoint
![image](https://user-images.githubusercontent.com/26767857/115206224-aaa2db80-a0fa-11eb-92b5-57952d6857d9.png)

Endpoint take in a plateau and a list of rovers as input.
![image](https://user-images.githubusercontent.com/26767857/115206637-14bb8080-a0fb-11eb-8ed7-7dc4060b81af.png)

Then returns a list of string rovers
![image](https://user-images.githubusercontent.com/26767857/115206789-3fa5d480-a0fb-11eb-8dc0-31403ea13274.png)

The project can also be exucuted in a docker container.

## To Review Angular UI
Project can be reviewed using visual studio code
You need angular/cli version 8 installed
The API endpoint can be updated in environment.ts


### How to execute UI?
You can execute project from the VS Code using ng serve
A form UI will POPUP to all allow user to add commands
![image](https://user-images.githubusercontent.com/26767857/115207507-f4d88c80-a0fb-11eb-8e3f-ace64a0feb05.png)

The use can add Plateau and multiple rovers by using the (add rover) button. Once a valid rover is added, a user can view the rover on the table and is allowed to deploy one or more rovers.
![image](https://user-images.githubusercontent.com/26767857/115208144-8d6f0c80-a0fc-11eb-8309-b67dfc158677.png)

Once deployed the last position column with be updated with the valid value
![image](https://user-images.githubusercontent.com/26767857/115208364-c27b5f00-a0fc-11eb-8720-9c4b6dab4719.png)





