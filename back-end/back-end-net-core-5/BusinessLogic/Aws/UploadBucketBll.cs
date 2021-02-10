using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end_net_core_5.Utils.Enums;

namespace back_end_net_core_5.BusinessLogic
{
    public class UploadBucketBll : IUploadBucketBll
    {
        private string bucketName = Environment.GetEnvironmentVariable("AWS_BUCKET_NAME");
        private const string keyName = "apk_teste";
        private const string filePath = @"C:\Users\SUFRAMA\Downloads\app_sample_gertec_rede.apk";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        ITransferUtility _tranferUtily;

        //IAmazonS3 s3Client;     
        public UploadBucketBll(ITransferUtility tranferUtily)
        {
            _tranferUtily = tranferUtily;
        }

        public int UploadItemToBucket()
        {
            try { 
                // Option 4. Specify advanced settings.
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.StandardInfrequentAccess, //define a durabilidade do arquivo na aws
                    PartSize = 1091456, // 10 MB.
                    CannedACL = S3CannedACL.Private
                  //CannedACL = S3CannedACL.PublicRead
                };

                _tranferUtily.Upload(fileTransferUtilityRequest);
                return (int)RestResponse.SUCCESS;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
                return (int)RestResponse.SERVER_ERROR;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
                return (int)RestResponse.SERVER_ERROR;
            }

        }
    }
}

