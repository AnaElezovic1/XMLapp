// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/accomodation.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

public static partial class AccomodationService
{
  static readonly string __ServiceName = "AccomodationService";

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
  {
    #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
    if (message is global::Google.Protobuf.IBufferMessage)
    {
      context.SetPayloadLength(message.CalculateSize());
      global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
      context.Complete();
      return;
    }
    #endif
    context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
  }

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static class __Helper_MessageCache<T>
  {
    public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
  }

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
  {
    #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
    if (__Helper_MessageCache<T>.IsBufferMessage)
    {
      return parser.ParseFrom(context.PayloadAsReadOnlySequence());
    }
    #endif
    return parser.ParseFrom(context.PayloadAsNewBuffer());
  }

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::Google.Protobuf.WellKnownTypes.Empty> __Marshaller_google_protobuf_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Google.Protobuf.WellKnownTypes.Empty.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::AccomodationList> __Marshaller_AccomodationList = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::AccomodationList.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::AccomodationId> __Marshaller_AccomodationId = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::AccomodationId.Parser));
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Marshaller<global::Accomodation> __Marshaller_Accomodation = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Accomodation.Parser));

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AccomodationList> __Method_GetAll = new grpc::Method<global::Google.Protobuf.WellKnownTypes.Empty, global::AccomodationList>(
      grpc::MethodType.Unary,
      __ServiceName,
      "GetAll",
      __Marshaller_google_protobuf_Empty,
      __Marshaller_AccomodationList);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::AccomodationId, global::Accomodation> __Method_GetById = new grpc::Method<global::AccomodationId, global::Accomodation>(
      grpc::MethodType.Unary,
      __ServiceName,
      "GetById",
      __Marshaller_AccomodationId,
      __Marshaller_Accomodation);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::Accomodation, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Create = new grpc::Method<global::Accomodation, global::Google.Protobuf.WellKnownTypes.Empty>(
      grpc::MethodType.Unary,
      __ServiceName,
      "Create",
      __Marshaller_Accomodation,
      __Marshaller_google_protobuf_Empty);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::Accomodation, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Delete = new grpc::Method<global::Accomodation, global::Google.Protobuf.WellKnownTypes.Empty>(
      grpc::MethodType.Unary,
      __ServiceName,
      "Delete",
      __Marshaller_Accomodation,
      __Marshaller_google_protobuf_Empty);

  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  static readonly grpc::Method<global::Accomodation, global::Google.Protobuf.WellKnownTypes.Empty> __Method_Update = new grpc::Method<global::Accomodation, global::Google.Protobuf.WellKnownTypes.Empty>(
      grpc::MethodType.Unary,
      __ServiceName,
      "Update",
      __Marshaller_Accomodation,
      __Marshaller_google_protobuf_Empty);

  /// <summary>Service descriptor</summary>
  public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
  {
    get { return global::AccomodationReflection.Descriptor.Services[0]; }
  }

  /// <summary>Base class for server-side implementations of AccomodationService</summary>
  [grpc::BindServiceMethod(typeof(AccomodationService), "BindService")]
  public abstract partial class AccomodationServiceBase
  {
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::AccomodationList> GetAll(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::Accomodation> GetById(global::AccomodationId request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> Create(global::Accomodation request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> Delete(global::Accomodation request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::System.Threading.Tasks.Task<global::Google.Protobuf.WellKnownTypes.Empty> Update(global::Accomodation request, grpc::ServerCallContext context)
    {
      throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
    }

  }

  /// <summary>Client for AccomodationService</summary>
  public partial class AccomodationServiceClient : grpc::ClientBase<AccomodationServiceClient>
  {
    /// <summary>Creates a new client for AccomodationService</summary>
    /// <param name="channel">The channel to use to make remote calls.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public AccomodationServiceClient(grpc::ChannelBase channel) : base(channel)
    {
    }
    /// <summary>Creates a new client for AccomodationService that uses a custom <c>CallInvoker</c>.</summary>
    /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public AccomodationServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
    {
    }
    /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    protected AccomodationServiceClient() : base()
    {
    }
    /// <summary>Protected constructor to allow creation of configured clients.</summary>
    /// <param name="configuration">The client configuration.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    protected AccomodationServiceClient(ClientBaseConfiguration configuration) : base(configuration)
    {
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::AccomodationList GetAll(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetAll(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::AccomodationList GetAll(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_GetAll, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::AccomodationList> GetAllAsync(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetAllAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::AccomodationList> GetAllAsync(global::Google.Protobuf.WellKnownTypes.Empty request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_GetAll, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Accomodation GetById(global::AccomodationId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetById(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Accomodation GetById(global::AccomodationId request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_GetById, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Accomodation> GetByIdAsync(global::AccomodationId request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return GetByIdAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Accomodation> GetByIdAsync(global::AccomodationId request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_GetById, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Google.Protobuf.WellKnownTypes.Empty Create(global::Accomodation request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return Create(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Google.Protobuf.WellKnownTypes.Empty Create(global::Accomodation request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_Create, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> CreateAsync(global::Accomodation request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return CreateAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> CreateAsync(global::Accomodation request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_Create, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Google.Protobuf.WellKnownTypes.Empty Delete(global::Accomodation request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return Delete(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Google.Protobuf.WellKnownTypes.Empty Delete(global::Accomodation request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_Delete, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> DeleteAsync(global::Accomodation request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return DeleteAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> DeleteAsync(global::Accomodation request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_Delete, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Google.Protobuf.WellKnownTypes.Empty Update(global::Accomodation request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return Update(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual global::Google.Protobuf.WellKnownTypes.Empty Update(global::Accomodation request, grpc::CallOptions options)
    {
      return CallInvoker.BlockingUnaryCall(__Method_Update, null, options, request);
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> UpdateAsync(global::Accomodation request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
    {
      return UpdateAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
    }
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public virtual grpc::AsyncUnaryCall<global::Google.Protobuf.WellKnownTypes.Empty> UpdateAsync(global::Accomodation request, grpc::CallOptions options)
    {
      return CallInvoker.AsyncUnaryCall(__Method_Update, null, options, request);
    }
    /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    protected override AccomodationServiceClient NewInstance(ClientBaseConfiguration configuration)
    {
      return new AccomodationServiceClient(configuration);
    }
  }

  /// <summary>Creates service definition that can be registered with a server</summary>
  /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  public static grpc::ServerServiceDefinition BindService(AccomodationServiceBase serviceImpl)
  {
    return grpc::ServerServiceDefinition.CreateBuilder()
        .AddMethod(__Method_GetAll, serviceImpl.GetAll)
        .AddMethod(__Method_GetById, serviceImpl.GetById)
        .AddMethod(__Method_Create, serviceImpl.Create)
        .AddMethod(__Method_Delete, serviceImpl.Delete)
        .AddMethod(__Method_Update, serviceImpl.Update).Build();
  }

  /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
  /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
  /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
  /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
  [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
  public static void BindService(grpc::ServiceBinderBase serviceBinder, AccomodationServiceBase serviceImpl)
  {
    serviceBinder.AddMethod(__Method_GetAll, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Google.Protobuf.WellKnownTypes.Empty, global::AccomodationList>(serviceImpl.GetAll));
    serviceBinder.AddMethod(__Method_GetById, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::AccomodationId, global::Accomodation>(serviceImpl.GetById));
    serviceBinder.AddMethod(__Method_Create, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Accomodation, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.Create));
    serviceBinder.AddMethod(__Method_Delete, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Accomodation, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.Delete));
    serviceBinder.AddMethod(__Method_Update, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Accomodation, global::Google.Protobuf.WellKnownTypes.Empty>(serviceImpl.Update));
  }

}
#endregion
