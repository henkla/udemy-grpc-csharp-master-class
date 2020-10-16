// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: greeting.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Greet {
  public static partial class GreetingService
  {
    static readonly string __ServiceName = "greet.GreetingService";

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

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

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

    static readonly grpc::Marshaller<global::Greet.GreetingRequest> __Marshaller_greet_GreetingRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Greet.GreetingRequest.Parser));
    static readonly grpc::Marshaller<global::Greet.GreetingResponse> __Marshaller_greet_GreetingResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Greet.GreetingResponse.Parser));
    static readonly grpc::Marshaller<global::Greet.GreetManyTimesRequest> __Marshaller_greet_GreetManyTimesRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Greet.GreetManyTimesRequest.Parser));
    static readonly grpc::Marshaller<global::Greet.GreetManyTimesResponse> __Marshaller_greet_GreetManyTimesResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Greet.GreetManyTimesResponse.Parser));
    static readonly grpc::Marshaller<global::Greet.LongGreetRequest> __Marshaller_greet_LongGreetRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Greet.LongGreetRequest.Parser));
    static readonly grpc::Marshaller<global::Greet.LongGreetResponse> __Marshaller_greet_LongGreetResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Greet.LongGreetResponse.Parser));
    static readonly grpc::Marshaller<global::Greet.GreetEveryoneRequest> __Marshaller_greet_GreetEveryoneRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Greet.GreetEveryoneRequest.Parser));
    static readonly grpc::Marshaller<global::Greet.GreetEveryoneResponse> __Marshaller_greet_GreetEveryoneResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Greet.GreetEveryoneResponse.Parser));

    static readonly grpc::Method<global::Greet.GreetingRequest, global::Greet.GreetingResponse> __Method_Greet = new grpc::Method<global::Greet.GreetingRequest, global::Greet.GreetingResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Greet",
        __Marshaller_greet_GreetingRequest,
        __Marshaller_greet_GreetingResponse);

    static readonly grpc::Method<global::Greet.GreetManyTimesRequest, global::Greet.GreetManyTimesResponse> __Method_GreetManyTimes = new grpc::Method<global::Greet.GreetManyTimesRequest, global::Greet.GreetManyTimesResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "GreetManyTimes",
        __Marshaller_greet_GreetManyTimesRequest,
        __Marshaller_greet_GreetManyTimesResponse);

    static readonly grpc::Method<global::Greet.LongGreetRequest, global::Greet.LongGreetResponse> __Method_LongGreet = new grpc::Method<global::Greet.LongGreetRequest, global::Greet.LongGreetResponse>(
        grpc::MethodType.ClientStreaming,
        __ServiceName,
        "LongGreet",
        __Marshaller_greet_LongGreetRequest,
        __Marshaller_greet_LongGreetResponse);

    static readonly grpc::Method<global::Greet.GreetEveryoneRequest, global::Greet.GreetEveryoneResponse> __Method_GreetEveryone = new grpc::Method<global::Greet.GreetEveryoneRequest, global::Greet.GreetEveryoneResponse>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "GreetEveryone",
        __Marshaller_greet_GreetEveryoneRequest,
        __Marshaller_greet_GreetEveryoneResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Greet.GreetingReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GreetingService</summary>
    [grpc::BindServiceMethod(typeof(GreetingService), "BindService")]
    public abstract partial class GreetingServiceBase
    {
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::Greet.GreetingResponse> Greet(global::Greet.GreetingRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Server streaming
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="responseStream">Used for sending responses back to the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>A task indicating completion of the handler.</returns>
      public virtual global::System.Threading.Tasks.Task GreetManyTimes(global::Greet.GreetManyTimesRequest request, grpc::IServerStreamWriter<global::Greet.GreetManyTimesResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Client streaming
      /// </summary>
      /// <param name="requestStream">Used for reading requests from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::Greet.LongGreetResponse> LongGreet(grpc::IAsyncStreamReader<global::Greet.LongGreetRequest> requestStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      /// Bi-directional
      /// </summary>
      /// <param name="requestStream">Used for reading requests from the client.</param>
      /// <param name="responseStream">Used for sending responses back to the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>A task indicating completion of the handler.</returns>
      public virtual global::System.Threading.Tasks.Task GreetEveryone(grpc::IAsyncStreamReader<global::Greet.GreetEveryoneRequest> requestStream, grpc::IServerStreamWriter<global::Greet.GreetEveryoneResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for GreetingService</summary>
    public partial class GreetingServiceClient : grpc::ClientBase<GreetingServiceClient>
    {
      /// <summary>Creates a new client for GreetingService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public GreetingServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GreetingService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public GreetingServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected GreetingServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected GreetingServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Greet.GreetingResponse Greet(global::Greet.GreetingRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Greet(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::Greet.GreetingResponse Greet(global::Greet.GreetingRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Greet, null, options, request);
      }
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Greet.GreetingResponse> GreetAsync(global::Greet.GreetingRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GreetAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Unary
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::Greet.GreetingResponse> GreetAsync(global::Greet.GreetingRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Greet, null, options, request);
      }
      /// <summary>
      /// Server streaming
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::Greet.GreetManyTimesResponse> GreetManyTimes(global::Greet.GreetManyTimesRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GreetManyTimes(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Server streaming
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::Greet.GreetManyTimesResponse> GreetManyTimes(global::Greet.GreetManyTimesRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_GreetManyTimes, null, options, request);
      }
      /// <summary>
      /// Client streaming
      /// </summary>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncClientStreamingCall<global::Greet.LongGreetRequest, global::Greet.LongGreetResponse> LongGreet(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return LongGreet(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Client streaming
      /// </summary>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncClientStreamingCall<global::Greet.LongGreetRequest, global::Greet.LongGreetResponse> LongGreet(grpc::CallOptions options)
      {
        return CallInvoker.AsyncClientStreamingCall(__Method_LongGreet, null, options);
      }
      /// <summary>
      /// Bi-directional
      /// </summary>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncDuplexStreamingCall<global::Greet.GreetEveryoneRequest, global::Greet.GreetEveryoneResponse> GreetEveryone(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GreetEveryone(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Bi-directional
      /// </summary>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncDuplexStreamingCall<global::Greet.GreetEveryoneRequest, global::Greet.GreetEveryoneResponse> GreetEveryone(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_GreetEveryone, null, options);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override GreetingServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GreetingServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(GreetingServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Greet, serviceImpl.Greet)
          .AddMethod(__Method_GreetManyTimes, serviceImpl.GreetManyTimes)
          .AddMethod(__Method_LongGreet, serviceImpl.LongGreet)
          .AddMethod(__Method_GreetEveryone, serviceImpl.GreetEveryone).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GreetingServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Greet, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Greet.GreetingRequest, global::Greet.GreetingResponse>(serviceImpl.Greet));
      serviceBinder.AddMethod(__Method_GreetManyTimes, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Greet.GreetManyTimesRequest, global::Greet.GreetManyTimesResponse>(serviceImpl.GreetManyTimes));
      serviceBinder.AddMethod(__Method_LongGreet, serviceImpl == null ? null : new grpc::ClientStreamingServerMethod<global::Greet.LongGreetRequest, global::Greet.LongGreetResponse>(serviceImpl.LongGreet));
      serviceBinder.AddMethod(__Method_GreetEveryone, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Greet.GreetEveryoneRequest, global::Greet.GreetEveryoneResponse>(serviceImpl.GreetEveryone));
    }

  }
}
#endregion
