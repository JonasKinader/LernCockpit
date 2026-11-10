# LernCockpit

[🇬🇧 English](#english) | [🇩🇪 Deutsch](#deutsch)

## Screenshots

<img width="454" height="227" alt="image" src="https://github.com/user-attachments/assets/ca7e4df6-18ff-4083-86ce-1bd3cc827730" /><img width="454" height="225" alt="image" src="https://github.com/user-attachments/assets/a1dac8e9-46e3-488c-81f5-ebbc821470fa" />
<img width="454" height="227" alt="image" src="https://github.com/user-attachments/assets/21aeccbe-3eb1-4b7f-97f0-7600129f1737" /><img width="454" height="227" alt="image" src="https://github.com/user-attachments/assets/f62d96fa-c6d7-4c17-ab90-211d2af8eb79" />
<details>
<summary>More Screenshots</summary>
<img width="454" height="226" alt="image" src="https://github.com/user-attachments/assets/f5c76a5e-9795-4e9f-ae02-67f2221706ce" /><img width="454" height="226" alt="image" src="https://github.com/user-attachments/assets/ac7c0359-028e-48cc-a215-9fae658d7e8e" />
<img width="454" height="224" alt="image" src="https://github.com/user-attachments/assets/612798b9-919e-4e47-aae4-f65502015f13" /><img width="454" height="245" alt="image" src="https://github.com/user-attachments/assets/d0ddc43f-ca4b-45e8-9ea0-5c3858e324c0" />
<img width="454" height="286" alt="image" src="https://github.com/user-attachments/assets/3c2e5d5d-4326-4155-9ba8-45ea2533a585" /><img width="454" height="211" alt="image" src="https://github.com/user-attachments/assets/caed2b13-98df-451a-9030-92badab5b170" />
<img width="454" height="305" alt="image" src="https://github.com/user-attachments/assets/8834b347-ec4a-4665-b019-09cabc0027c1" /><img width="454" height="248" alt="image" src="https://github.com/user-attachments/assets/45824fef-3dac-4996-a772-d85314d6dd88" />
<img width="454" height="224" alt="image" src="https://github.com/user-attachments/assets/74931b36-955e-457b-ae1c-69d8fa51560c" /><img width="454" height="224" alt="image" src="https://github.com/user-attachments/assets/890ec39d-f2a5-468b-95d9-b4ca47bd52a2" />
<img width="454" height="246" alt="image" src="https://github.com/user-attachments/assets/895f3dad-5998-4363-9a98-72fecaa626fb" /><img width="454" height="251" alt="image" src="https://github.com/user-attachments/assets/93648766-f698-43a1-8017-e24e527129e7" />
</details>

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
