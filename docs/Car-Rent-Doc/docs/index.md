# Car Rent Api Documentation

![BigPicture](./images/BigPicture.JPG)

# Context Diagram

![ContextDiagram](./images/ContextDiagram.JPG)

Der Kunde kann bei einer Reservation ein Auto aus einer bestimmten Klasse wählen. Bei der Reservation muss er die Mietdauer und das Anfangsdatum angeben. 
Der Sachbearbeiter kann den Fuhrpark verwalten. Zudem kann er die Kunden im System verwalten und suchen.

# Container Diagram

![ContainerDiagram](./images/ContainerDiagram.JPG)

Die Webaplikation macht API calls zur RestApi über welche Datenbankoperationen ausgeführt werden. Je nach Aufruf werden Daten geschrieben oder gelesen.

# Component Diagram
![ContextDiagram](./images/ComponentDiagram.JPG)

Der Kunde kann ein Auto auswählen und eine Reservation tätigen. Bei Abholung des Wagens muss der Kunde einen Vertrag unterschreiben. Die Reservation beinhaltet das ausgewählte Fahrzeug, das Reservationsdatum, die Mietdauer, die Gesamtkosten und der Mietzeitpunkt. 