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

## Configuration & Setup (For Lecturer)
To run this application securely, the sensitive Azure credentials have been removed from the source code. 

Before running `docker compose up`, please replace the placeholders in `docker-compose.yml` and `RaceDay_API/appsettings.json` with the following credentials:

**Azure SQL Connection String:**
*   **Placeholder:** `YOUR_SQL_PASSWORD_HERE`
*   **Actual Password:** `Siyabonga@1`

**Azure Blob Storage Connection String:**
*   **Placeholder:** `YOUR_STORAGE_KEY_HERE`
*   **Actual Key:** `GW/9iPOwGpohMfKVj+wQ4SkRN4AxrukR3ibMMYtt5uQ41zDzuQec6YAazZ0WYW7i/p6vBDDeC8Ya+ASt3oCZYg==`

Once the placeholders are replaced, run:
`docker compose up --build -d`

### Installation & Setup

1. **Clone the repository:**
   ```bash
   git clone [Your GitHub Repo Link]
   cd RaceDay_API
