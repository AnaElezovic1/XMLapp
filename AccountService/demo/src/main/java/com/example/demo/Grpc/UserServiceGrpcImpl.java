package com.example.demo.Grpc;
import com.example.demo.model.UserApp;
import com.example.demo.service.UserService;
import com.example.demo.user.*;
import com.example.demo.user.UserAppServiceGrpc;
import io.grpc.stub.StreamObserver;
import org.apache.catalina.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component //dodao
public class UserServiceGrpcImpl extends UserAppServiceGrpc.UserAppServiceImplBase {
  private final UserService userService;

   // @Autowired uklonio
    public UserServiceGrpcImpl(UserService userService) {
        this.userService = userService;
    }
    @Override
    public void getUsers(GetUsersRequest request, StreamObserver<GetUsersResponse> responseObserver) {
        List<UserApp> userAppList = userService.getUsers();

        GetUsersResponse.Builder responseBuilder = GetUsersResponse.newBuilder();

        for (UserApp user : userAppList) {
            responseBuilder.addUsers(com.example.demo.user.UserApp.newBuilder()
                    .setId(user.getId())
                    .setUsername(user.getUsername())
                    .setEmail(user.getEmail())
                            .setAddress(user.getAdress())
                            .setPassword((user.getPassword()))
                    .build());
        }

        GetUsersResponse response = responseBuilder.build();

        responseObserver.onNext(response);
        responseObserver.onCompleted();
    }

    @Override
    public void addUser(AddUserRequest request, StreamObserver<AddUserResponse> responseObserver){



      UserApp newUser = mapUser(request.getUser());
      userService.addNewUser(newUser);

      AddUserResponse response = AddUserResponse.newBuilder().setSuccess(true).build();

        responseObserver.onNext(response);
        responseObserver.onCompleted();
       

    }

    private UserApp mapUser(com.example.demo.user.UserApp user){
        UserApp mappedUser = new UserApp();
        mappedUser.setId(user.getId());
        mappedUser.setUsername(user.getUsername());
        mappedUser.setPassword(user.getPassword());
        mappedUser.setEmail(user.getEmail());
        mappedUser.setRole(com.example.demo.model.Role.valueOf(user.getRole().name()));
        mappedUser.setAdress(user.getAddress());
        System.out.println("Mapped User Username: " + mappedUser.getUsername());
        System.out.println("Mapped User: " + mappedUser.toString());
        return mappedUser;
    }
}
