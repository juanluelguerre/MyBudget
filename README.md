# MyBudget
Just a sample ASP.NET Core Web App to show basic DevOps Steps, using Azure DevOps and Jenkins: 
- [Azure DevOps](https://elguerre.com/2019/04/28/from-github-to-azure-app-service-through-azure-devops-pipelines)
- [Jenkins](https://elguerre.com/2019/05/25/from-github-to-azure-app-service-through-jenkins-pipelines)

# MyBudget.API and MyBudget.Application
New sample API using CQRS + Event Sourcing and DDD using following library:

API Project:
- Think controllers
- Middlewares
- Filters


Application Project group by Domain (Customers and Accounts):
- Swagger
- Entity Framework Core for Commands.
- [Dapper](https://github.com/StackExchange/Dapper/tree/master/Dapper) for Queries
- [MediatR](https://github.com/jbogard/MediatR) as Mediator/Brocker
- [Marten](https://github.com/JasperFx/marten) to save events using PostgreSQL


**Notes**:
- Models. Used by Controllers as Input requests/dtos.
- ViewModels. Used by Controlloers as output as responses/projections. Also know as QueryResults by QueryHandlers.



# Next steps:
- Apply Service BUS and [RabitMQ](https://www.rabbitmq.com/) as Message Broker
- Save Events to Azure  [Streamstone](https://github.com/yevhen/Streamstone)
- [Apache Kafka](https://kafka.apache.org/)
- Everhing using Docker
- Everhing using Kubernetes

