using CareerCloud.gRPC.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantJobApplicationService;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService : ApplicantJobApplicationServiceBase
    {
        public override Task<ApplicantJobApplication> GetApplicantJobApplication(IdRequest1 request, ServerCallContext context)
        {
            return base.GetApplicantJobApplication(request, context);
        }
    }
}
