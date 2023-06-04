package com.example.demo;

import com.example.demo.Grpc.UserServiceGrpcImpl;
import com.example.demo.service.UserService;
import io.grpc.Server;
import io.grpc.ServerBuilder;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.ConfigurableApplicationContext;
import org.springframework.context.annotation.Bean;

import java.io.IOException;

@SpringBootApplication
public class AccountServiceApplication {

    public static void main(String[] args) throws IOException, InterruptedException {
        int port = 50051; // Specificirajte broj porta za server

//        final UserService userService;
//
//        GrpcServer(UserService userService){
//            this.userService = userService;
//        }
        UserServiceGrpcImpl userServiceGrpcImpl = getUserServiceGrpcImpl();

        // Kreirajte server
        Server server = ServerBuilder.forPort(port)
                .addService(userServiceGrpcImpl)
                .build();

        // Startujte server
        server.start();
        System.out.println("Server started on port " + port);

        // Sačekajte da se server završi
        server.awaitTermination();
    }

    private static UserServiceGrpcImpl getUserServiceGrpcImpl() {
        ConfigurableApplicationContext context = SpringApplication.run(AccountServiceApplication.class);
        UserService userService = context.getBean(UserService.class);
        return new UserServiceGrpcImpl(userService);
    }

}
