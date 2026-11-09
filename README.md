# RaceDay - Part 3 (MVC Web Application)

**Student Name: Siyabonga Msimango
**Student Number: ST10488360
**Module:** PROG6212
**Project:** RaceDay - Event Management System

##  Project Overview
This repository contains the final part of the RaceDay project. It includes a fully functional, containerized ASP.NET Core MVC Web Application that consumes the RaceDay RESTful API (built in Part 2). 

The application supports two distinct user roles (Organiser and Participant) with session-based authentication. It integrates with **Azure SQL Database** for data persistence and **Azure Blob Storage** for cloud-based image uploads. The entire application is containerized using Docker.

##  Technologies Used
*   **Frontend:** ASP.NET Core MVC (.NET 8.0), Razor Views, Bootstrap 5
*   **Backend API:** ASP.NET Core Web API (.NET 8.0)
*   **Database:** Microsoft Azure SQL Database (Entity Framework Core)
*   **Cloud Storage:** Microsoft Azure Blob Storage
*   **Containerization:** Docker & Docker Compose
*   **CI/CD:** GitHub Actions

##  Key Features
*   **Role-Based Navigation:** Distinct interfaces for Organisers and Participants.
*   **Session Management:** Secure login using API tokens stored in session cookies.
*   **Organiser Dashboard:** Create events, manage categories, and upload event banner images.
*   **Participant Interface:** Browse events, view details, and enroll in specific race categories.
*   **Cloud Integration:** All images are uploaded securely through the API to Azure Blob Storage.
*   **Dockerized Architecture:** Both the API and Web App run in isolated containers.

##  Getting Started

### Prerequisites
*   [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running.
*   Active Azure subscription (for SQL and Blob Storage).

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone [Your GitHub Repo Link]
   cd RaceDay_API
