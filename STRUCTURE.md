# Project Structure:
## Repository Pattern principles.
- `./src/Publisher.Api`: Layer for Publisher to 'publish the message'.
  - `./Publisher.Api/Controllers`: Control by Swagger to simulate a message.
    
- `./src/Consumer.App`: Simple didactic class just to simulate message consumption.
  
- `.src/Models.App`: Layer for MessageModel class.

- `./src/Repositories.App`: Used to isolate data access/services logic from the application's business logic.
  - `./src/Repositories.App/Interfaces`: Interfaces for message services repositories.

- `./src/Services.App`: Service as an intermediary between the Controller (presentation layer) and the Repository (RabbitMQ and Kafka Repositories)
  - `./src/Services.App/Interfaces`: Interfaces to intermediary layers.
