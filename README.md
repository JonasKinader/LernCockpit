# LernCockpit

[🇬🇧 English](#english) | [🇩🇪 Deutsch](#deutsch)

## Screenshots

[HIER DEINE SCREENSHOT-ZEILEN EINFÜGEN]

---

## English

A web app that helps apprentices and retraining students organize their learning material and track their progress. Trainers get their own dashboard to see how far their apprentices have come.

Built as a project during my retraining as an IT specialist for application development (Fachinformatiker Anwendungsentwicklung), 12/2025. The user interface is in German.

### Features

- **Sign-up and login** with hashed passwords (BCrypt)
- **Two roles:** apprentice and trainer (trainer role unlocked with an access code)
- **Learning topics** organized as folders with main topics and subtopics (create, edit, delete)
- **Notes and files** attached to each topic
- **Learning status** per topic: not started, started, practicing, understood
- **Trainer dashboard:** overview of all apprentices and their progress
- Login persists across page reloads
- Imprint and privacy policy pages

### Tech stack

- C# / .NET 9
- Blazor (Interactive Server)
- MudBlazor (UI components)
- Entity Framework Core with MySQL (Pomelo)
- BCrypt.Net for password hashing

### Getting started

Requirements: .NET 9 SDK and a local MySQL server (e.g. via XAMPP).

1. Clone the repository
   ```
   git clone https://github.com/JonasKinader/LernCockpit.git
   cd LernCockpit
   ```
2. Adjust the database connection in `appsettings.json` (`DefaultConnection`)
3. Create the database
   ```
   dotnet ef database update
   ```
4. Run the app
   ```
   dotnet run
   ```
5. Open the URL shown in the console in your browser (e.g. https://localhost:5001)

### What I learned

- Building my own login and role system in Blazor
- Implementing authentication with cookies and a custom `AuthenticationStateProvider`
- Storing the login state in the browser so users stay logged in after a page reload
- Designing a data model with relationships (users, topics, subtopics, files) using EF Core and migrations
- Storing passwords securely as hashes instead of plain text
- Moving logic into a service behind an interface

### Ideas for later

- Password reset
- Unit tests for the LearningService
- Design overhaul

---

## Deutsch

Eine Web-App, mit der Auszubildende und Umschüler ihren Lernstoff organisieren und ihren Fortschritt verfolgen. Ausbilder sehen in einem eigenen Dashboard, wie weit ihre Azubis sind.

Entstanden als Projekt während meiner Umschulung zum Fachinformatiker für Anwendungsentwicklung (12/2025).

### Funktionen

- **Registrierung und Login** mit gehashten Passwörtern (BCrypt)
- **Zwei Rollen:** Azubi und Ausbilder (Ausbilder-Rolle über einen Freischaltcode)
- **Lernthemen** als Ordner mit Haupt- und Unterthemen anlegen, bearbeiten und löschen
- **Notizen und Dateien** zu jedem Thema speichern
- **Lernstatus** pro Thema: Nicht begonnen, Begonnen, In Übung, Verstanden
- **Ausbilder-Dashboard:** Übersicht über alle Azubis und ihren Lernstand
- Login bleibt beim Neuladen der Seite erhalten
- Impressum- und Datenschutzseite

### Technik

- C# / .NET 9
- Blazor (Interactive Server)
- MudBlazor (UI-Komponenten)
- Entity Framework Core mit MySQL (Pomelo)
- BCrypt.Net für Passwort-Hashing

### Projekt starten

Voraussetzungen: .NET 9 SDK und ein lokaler MySQL-Server (z.B. über XAMPP).

1. Repository klonen
   ```
   git clone https://github.com/JonasKinader/LernCockpit.git
   cd LernCockpit
   ```
2. Verbindung zur Datenbank in `appsettings.json` anpassen (`DefaultConnection`)
3. Datenbank anlegen
   ```
   dotnet ef database update
   ```
4. App starten
   ```
   dotnet run
   ```
5. Im Browser die Adresse aus der Konsole öffnen (z.B. https://localhost:5001)

### Was ich dabei gelernt habe

- Ein eigenes Login- und Rollensystem in Blazor aufbauen
- Authentifizierung mit Cookies und einem eigenen `AuthenticationStateProvider` umsetzen
- Den Login-Status im Browser speichern, damit man nach dem Neuladen eingeloggt bleibt
- Datenbankmodell mit Beziehungen (Benutzer, Themen, Unterthemen, Dateien) mit EF Core und Migrationen
- Passwörter sicher als Hash speichern statt im Klartext
- Logik in einen Service mit Interface auslagern

### Ideen für später

- Passwort-zurücksetzen-Funktion
- Unit-Tests für den LearningService
- Design überarbeiten
