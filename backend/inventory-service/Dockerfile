FROM openjdk:17-jdk-alpine AS build

WORKDIR /app

COPY ./.mvn ./.mvn
COPY ./mvnw ./mvnw
COPY ./pom.xml ./pom.xml

RUN chmod +x ./mvnw

RUN ./mvnw dependency:go-offline

COPY . .

RUN ./mvnw clean package -DskipTests


FROM openjdk:17-jdk-alpine

WORKDIR /app

COPY --from=build /app/target/chat-service-0.0.1-SNAPSHOT.jar /app/service.jar

EXPOSE 8080

CMD ["sh", "-c", "java -jar /app/service.jar \
            --spring.datasource.url=${SPRING_DATASOURCE_URL} \
            --spring.datasource.username=${SPRING_DATASOURCE_USERNAME} \
            --spring.datasource.password=${SPRING_DATASOURCE_PASSWORD} \
            --spring.security.oauth2.resourceserver.jwt.issuer-uri=${SPRING_SECURITY_OAUTH2_RESOURCESERVER_JWT_ISSUER_URI} \
            --events.queues.event-queue=${EVENT_QUEUE_URL}"]