FROM openjdk:17
ADD target/demo-0.0.1-SNAPSHOT.jar app.jar
ENTRYPOINT ["java", "-jar", "/app.jar"]

# Konfiguracija za povezivanje sa PostgreSQL bazom
ENV SPRING_DATASOURCE_URL=jdbc:postgresql://localhost:5432/account
ENV SPRING_DATASOURCE_USERNAME=postgres
ENV SPRING_DATASOURCE_PASSWORD=curkepro00.
