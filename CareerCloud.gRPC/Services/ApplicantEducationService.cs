using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.ApplicantEducationService;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService : ApplicantEducationServiceBase
    {
        public override Task<ApplicantEducation> GetApplicantEducation(IdRequest request, ServerCallContext context)
        {
            var logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
            ApplicantEducationPoco poco = logic.Get(Guid.Parse(request.Id));
            if(poco == null)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new Task<ApplicantEducation>(() => { return TranslateFromPoco(poco); });
        }
        public override Task<Empty> DeleteApplicantEducation(ApplicantEducations request, ServerCallContext context)
        {
            var logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
            List<ApplicantEducationPoco> pocos = new List<ApplicantEducationPoco>();
            foreach(ApplicantEducation proto in request.AppEdu)
            {
                pocos.Add(TranslateFromProto(proto));
            }
            logic.Delete(pocos.ToArray());
            return Task.FromResult(new Empty());
        }
        private ApplicantEducation TranslateFromPoco(ApplicantEducationPoco poco)
        {
            return new ApplicantEducation()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Major = poco.Major,
                CertificateDiploma = poco.CertificateDiploma,
                StartDate = poco.StartDate == null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate),
                CompletionDate = poco.CompletionDate == null ? null : Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                CompletionPercent = (int)poco.CompletionPercent
            };
        }

        private ApplicantEducationPoco TranslateFromProto(ApplicantEducation proto)
        {
            return new ApplicantEducationPoco
            {
                Id = Guid.Parse(proto.Id),
                Applicant = Guid.Parse(proto.Applicant),
                Major = proto.Major,
                CertificateDiploma = proto.CertificateDiploma,
                StartDate = proto.StartDate == null ? null : (proto.StartDate.ToDateTime()),
                CompletionDate = proto.CompletionDate == null ? null : (proto.CompletionDate.ToDateTime()),
                CompletionPercent = (byte)proto.CompletionPercent
            };
        }
    }
}
