# GTR Mobility Services API Integration

The API integrates the official **GTR (Gestió Telemàtica Ramadera)** platform services to streamline the complete management of animal movements via web services, enabling efficient and reliable communication between farms and authorities.

The GTR platform is designed to manage animal mobility through files or web services, especially useful for high-volume data processes, ensuring traceability and regulatory compliance.

!!! warning  "Note:"  
	A **mobility password** is required to access GTR services. It can be requested using the following form:  [Request Mobility Password](https://agricultura.gencat.cat/web/.content/07-ramaderia/gestio-telematica-ramadera/documents/fitxers_estatics/sollicitud-contrasenya-GTR.doc)

## Integrated Services

### 1. Guide Registration (Alta de Guías)

Allows mass registration of mobility guides for animal transport.

- Validates business rules and animal data (species, number, origin/destination farms).
- Supports scheduling, transport info, and creation of DST documents.
- Enables efficient batch creation of transport guides.

!!! info "**GTR Official Service Documentation:**"
	[Guide Registration Service Documentation (PDF)](https://agricultura.gencat.cat/web/.content/07-ramaderia/gestio-telematica-ramadera/documents/fitxers_estatics/Cataleg-servei-alta-guies.pdf)



### 2. Movement Confirmation (Confirmación de Movimientos de Entrada)

Enables confirmation of animal arrivals at the destination farm.

- Updates guide and movement status in the system.
- Supports time stamping and optional comments.
- Ensures traceability and validation of animal transports.

!!! info "**GTR Official Service Documentation:**"
	[Movement Confirmation Service Documentation (PDF)](https://agricultura.gencat.cat/web/.content/07-ramaderia/gestio-telematica-ramadera/documents/fitxers_estatics/Cataleg-servei-confirmacio-moviments-dentrada.pdf)



### 3. Guide Update (Actualización de Guías)

Allows updates to existing mobility guides for corrections or status changes.

- Modify data of DST guides downloaded and processed on mobile apps.
- Changes guide status to “Issued” and mobility status to “Closed” after update.
- Communication via REST/HTTPS with specific endpoints for download and update.
- Validates authentication and business rules before processing.
- Returns detailed error codes for various validation failures.


!!! info "**GTR Official Service Documentation:**"
	[Guide Update Service Documentation (PDF)](https://agricultura.gencat.cat/web/.content/07-ramaderia/gestio-telematica-ramadera/documents/fitxers_estatics/Cataleg-servei-guies-de-mobilitat.pdf)
