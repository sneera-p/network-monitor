# Unified Network Monitoring Data Platform

A microservice-based application designed to collect, store, report on, and display network monitoring data from various sources in a standardized format.

## Architecture

The application follows a microservice architecture interacting with a centralized SurrealDB instance and utilizing shared libraries for core logic and data structures.

## Components

* **Database:** Utilizes **SurrealDB** for data storage.
    * `network` database: Stores time-series (Events, Incidents) and static device data with graph relationships.
    * `reports` database: Stores generated PDF reports.
    * `users` database: Stores user authentication data.
* **Ingestor:**
    * Collects data from external monitoring services (Zabbix, Site24x7, Avamar, vSphere, Citrix).
    * Uploads data to the `network` database via **WebSocket**.
    * Built with **Async Python** and **Cython** for shared library integration.
* **Reporter:**
    * Generates scheduled reports (Daily, Monthly) based on stored data.
    * Queries data from the `network` database via **WebSocket**.
    * Built with **.NET 8 C#** using `[LibraryImport]` for shared library integration.
* **Dashboard:**
    * Provides a web interface for visualizing device Events and Incidents.
    * Built as a web app using **SolidJS**, **Vite**, and **TypeScript**.
    * Uses **Chart.js** for graphing.
    * Loads shared libraries via **WASM**.
    * (Future consideration: Desktop app packaging using Tauri).

## Shared Libraries

* **Core (C):** Contains common logic shared across multiple services. Compiled with `gcc` (for Python/C#) and `emcc` (for WASM).
* **Protos:** Defines shared data structures using **Protobuf**. Compiled to relevant languages using `protoc` and `protoc-c`.

## Folder Structure

* **Core/**         # Shared logic written in C
* **Protos/**       # Protobuf .proto files
* **Web/**          # Dashboard service (SolidJS, Vite, TypeScript)
* **Report/**       # Reporter service (.NET 8 C#)
* **Ingest/**       # Ingestor service (Async Python, Cython)

## Getting Started

1.  **Prerequisites:** Ensure you have Docker, Python (with pip), .NET SDK 8+, Node.js (with npm/yarn/pnpm), gcc, emcc, and protoc installed.
2.  **Setup Database:** Start the SurrealDB Docker container.
3.  **Build Shared Libraries:**
    * Compile the C `Core` library for your target environments (Linux .so, Windows .dll, WASM .wasm).
    * Compile the `Protos` files to generate language-specific code.
4.  **Build/Run Services:** Navigate into each service's directory (`Ingest/`, `Report/`, `Web/`) and follow their specific build and run instructions (detailed in their respective READMEs - *TODO: Add service-specific READMEs*).
5.  **Configuration:** Each service will require configuration (e.g., database connection URL, monitoring service API keys). Use environment variables or configuration files (refer to service-specific documentation).

## Technologies Used

* **Database:** SurrealDB
* **Languages:** Python, C#, SolidJS, TypeScript, C, Protobuf
* **Frameworks/Libraries:** asyncio, Cython, .NET 8, SolidJS, Vite, Chart.js, WASM, `websockets` (Python), `Discord.Net` / `DSharpPlus` / `NetCord` (C# - for Discord integration if used), `QuestPDF` (C# - for PDF reporting), `SurrealDB.Net`, `SurrealDB.Driver` (C# driver), `surrealdb.py` (Python driver)
* **Tooling:** Docker, gcc, emcc, protoc, npm/yarn/pnpm, pip, dotnet CLI

---