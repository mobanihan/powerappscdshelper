using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PowerAppCDSHelper.Utils
{
    public class WebUtils
    {
        /* POST https://admin.powerapps.com/Authentication/RefreshAccessToken?audience=https%3A//IntegratorApp.com HTTP/1.1
        Host: admin.powerapps.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:70.0) Gecko/20100101 Firefox/70.0
Accept: application/json, text/plain, 
Accept-Language: en-US,en;q=0.5
Accept-Encoding: gzip, deflate, br
RequestVerificationToken: MIIERQYJKoZIhvcNAQcCoIIENjCCBDICAQExCzAJBgUrDgMCGgUAMIICTwYJKoZIhvcNAQcBoIICQASCAjwwggI4BgkqhkiG9w0BBwOgggIpMIICJQIBADGCAcAwggG8AgEAMIGjMIGLMQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMRUwEwYDVQQLEwxNaWNyb3NvZnQgSVQxHjAcBgNVBAMTFU1pY3Jvc29mdCBJVCBUTFMgQ0EgNQITLQAMuF0/5uF7Eb5R8wAAAAy4XTANBgkqhkiG9w0BAQcwAASCAQBDTrZMU9DRqvQb2TIUZYMXju8qmq+kUQ3VQxiBTvyEIBhojNoA7foKl1N8Vru/WK8x9Xd/CTviXgr/8tZXB5PljRMJw6o0KD6uWch+yJZ4Yle1pzWZhYL31vu4/wxvXER8dsos62baFUmcyN1efV/7QqiEhkxFnanIgD8xI0T/wgOepWCEoCKMWdjBmLA9zQSvPg8yjZRcrxlfkyGYHd9Jk6vy7RaKNQJkOWW4ZJgZtqTliUFGbUPpCbXwiDCfmHPEmiMnjxXn7XremI+PWfIi/SAfLqZJQVYuYhIlhN/qbip2VwU8SinOfomEnAIEtEaV8LkRo67QlSUTS40w0EuJMFwGCSqGSIb3DQEHATAdBglghkgBZQMEAQIEEJEZ6kOfrv1wgrBLUmg+xvmAMCheZ9rPXeZdgVF5e0XSadVF3HjTZ8EVP/pXl8XpkMxgngSVpeabdOMc7zHd3DLpOzGCAcswggHHAgEBMIGjMIGLMQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMRUwEwYDVQQLEwxNaWNyb3NvZnQgSVQxHjAcBgNVBAMTFU1pY3Jvc29mdCBJVCBUTFMgQ0EgMgITIAAMuLryBJEcSnyNGAAAAAy4ujAJBgUrDgMCGgUAMA0GCSqGSIb3DQEBAQUABIIBAJPtcsox7M1pRuSVD4kF0ianzcYCGPsKvF4+0YqfZNIFLqm9HePfcp9XONoK1mwK4EPt7nEy4fAIbC6G08xn+7MKSkTJljw+BzBpbrWsi4uZNhF6UVhbNF4rHG/TRe0IjHGB8UthPMIiKvOaFEkneS5bEc+GLM4+ObgHVEjSjV+6mH0s6nlo+nesps3exdYM8eIrbEj4mcCsG0uIFdhKmBCO/aX//IFr6FJ1k99AoZ06GBEkXIB5CJADXq2ceno0ntaIuiW0Mapf6ortpVKjrG5aNDIYHKqEeVjhIeMaGpPeFpyn/yyq/06ftzHEztuljR3KOaroiyaSUkNf7muA+80=,MIIEZQYJKoZIhvcNAQcCoIIEVjCCBFICAQExCzAJBgUrDgMCGgUAMIICbwYJKoZIhvcNAQcBoIICYASCAlwwggJYBgkqhkiG9w0BBwOgggJJMIICRQIBADGCAcAwggG8AgEAMIGjMIGLMQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMRUwEwYDVQQLEwxNaWNyb3NvZnQgSVQxHjAcBgNVBAMTFU1pY3Jvc29mdCBJVCBUTFMgQ0EgNQITLQAMuF0/5uF7Eb5R8wAAAAy4XTANBgkqhkiG9w0BAQcwAASCAQCM2ko4+mbM2vkJovOIFLT+k+ijCpwsDctYh3veqO7jtwbRMFP/PhWzNkq28P57cqtOpXxYhD8eo1Ri22x2jEQBvu5C35GYOpKJeimsYZdhk/V//Ruurk/t2/Vf3mjw0yB4lagNgbrhBe664qIMaIWytcoFM6+whT1LBOnqFlOAT2E5ujWmEJKwMk/6IS2yh0ZEZuGbPVp+91SIkh7ErkyHw/IWkwpjvceJjwMjftrvoagL6U02j2H3TlVz43HDSCSRA6xCoTPMVAvP7rhBxtB1zfl3Ma4+g2wV6imodkFsg9vbGh74vKI5CThbhlt/Lr7Tv0BvN+IZdzfftuvppXuxMHwGCSqGSIb3DQEHATAdBglghkgBZQMEAQIEEOy7Dv6YiQoRMJbQdmsrC9OAUKGQ9KiSsHCX8tQoh8XNWAXb1GfXxRvWr+2whURqU166V60txis/XeRx7L99vjQthxepXZoELxzYhx6VJZhvdD18kp35+Jfh1Ce/9aqMiiAOMYIByzCCAccCAQEwgaMwgYsxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xFTATBgNVBAsTDE1pY3Jvc29mdCBJVDEeMBwGA1UEAxMVTWljcm9zb2Z0IElUIFRMUyBDQSAyAhMgAAy4uvIEkRxKfI0YAAAADLi6MAkGBSsOAwIaBQAwDQYJKoZIhvcNAQEBBQAEggEAdCnBCAJiKr0ex5Z0J/niaCfHgjpx1YsXKPdNKN+LfmdgEQFPadPs+C/GMr6b0TRp19d9P6W6wNPmUVaWslQddBSAIjC9+nYsiE28p7MOxxLutJnNC6kmodcwnandJAyADxZTuZaM1Kw4CzFhQ7cqI5qxWRGJoc0x3dPngFZX9HpWR6Hm0APsrYuOhhTVi5v8MvPdeaTV9gG/OZYjnoR9tpt4anmadA/YmB5GALEM1gx2mPfKoTW241lhE0BgQfBey340VPZgyPrXwyzbpV+0hlS7T/UQCsbd2qYhM/kdQRjQVTAQQ56jxiSnjJRtYWmhGk14eZRw0WXoEjbiQWJalw==
x-ms-client-request-id: b9a407de-e2b4-43e7-8f2f-7a6ed87f68f6
x-ms-client-session-id: 7e309434-b874-4e6d-a04a-f99dd74e0b63
Origin: https://admin.powerapps.com
Connection: keep-alive
Referer: https://admin.powerapps.com/dataintegration/projects
Cookie: PALogin=chunks:2; PALoginC1=MIIR6QYJKoZIhvcNAQcCoIIR2jCCEdYCAQExCzAJBgUrDgMCGgUAMIIP8wYJKoZIhvcNAQcBoIIP5ASCD-Awgg_cBgkqhkiG9w0BBwOggg_NMIIPyQIBADGCAcAwggG8AgEAMIGjMIGLMQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMRUwEwYDVQQLEwxNaWNyb3NvZnQgSVQxHjAcBgNVBAMTFU1pY3Jvc29mdCBJVCBUTFMgQ0EgNQITLQAMuF0_5uF7Eb5R8wAAAAy4XTANBgkqhkiG9w0BAQcwAASCAQBYkc_LBZJgyUFJUiN1UscS_a60vly8FCBYAkqZ7uLL1oGO6f_gv677mwvlj94NlwdeuT5ugN8x5WOOideefiYH40h8Nozs_Oobl6cmVw-2fIYX-_uurbiscYEbmX_nXhMk0vTuea-QiMB-jTnnlX9Yp77056H6GExFiepXwhrD0l69nh1PJrAMWQh5ctYBKljW3TCZMReal0duuoIxmD-pKZkSvli-1S2oUivjWQPdZ-9mdfZm-PqiDqczcicVFUBZBdaqH-CSUNQKrtO0_LfQcvW4FVMcZVnFhDeBLDKp4csuMflZSqWyG6L6IbzpKePzvdhwqmtb00dQW0hBk4ViMIIN_gYJKoZIhvcNAQcBMB0GCWCGSAFlAwQBAgQQ0QKBlE6Ek20-c3MXEY6huoCCDdD5HSUPZsMG1-ch14B2QvSYL3_TVjkuu5Y-MsjgDBZIPYrhorkFUt5KcdCNUFVFeMtY5Yq7wkg72V4ssIpzOd4LjWWbnhbn0Ik_HocAuf2ObUjT8oQCqFuqH3KxawyHqo8lmFUEEe8oR7KGkVmMrI7d2paWY4qXdAywz14FWn1qXtlVP1j9XYijK8Sn7AaduHCk4vfS9C4-k-m50lBtkAZoJKhWId2nMjToBPqJKAtlVEyYPVAuPcEj7HQPxTlW-x7AWK5D4zhvkYLzBCMHb4IE1TXtsgH7Ph8j9OJwg493ZWumfGUzSZOzJh1gK8kikKU4aTxcXo3c-_8jkJoyAuPjIvpkPqgI5geN7B4cVhu08Bi6kPI3uKnue4sDrVI5m2UhjE1iI7L1arx-cz5X705AnQHW3Q2KN-EjkQnVo4pBgEXe2ceJT0m04sdMtOeF5ie5n5ov1jZvky6RrDl6wcOyqfCraW-wrmLYPjwJRzMEIxePV0EsCa7INvHvAFQN5xLrfotJbQNmIwrVJVgULwfbe_fIeHxhkT0IibWtCYLDDj1GZDZwm_eewx0N8zzfGzZJvA2IPVuR7sG8McbdArYtKm9XyKIb_XjLIdtWHhdwbREwmY9T2mwK7m9A_GBwy-ibLYdV2VdnwhlXW7GgyKzlVyALhj-FZgXYt7OUE0pwnSitIXKguatIOA62sbcEZqxIPByF7tCT7o-RRtLY36IiobCH6ie0vsxRvjqsz3MU2YE0Sm9CZh-kisgrCJWX5e7htQKtfs0bleA1UoYtaEPgvKyfvs_zmYbRBAArGSu1WLfkwyA1TPRBze34kgtxnCk92VTZwxWzDizdarQfveuF_Ef1JTIAGDGzflfETWda7Zk7DaVHvZgIX3Z4lce_7kPoqI3d9fl7T2i3wY-qUCTcr6amvLAXjIMVtJTj17J9aSlHvtInAJJj2bDA8mpwrcdjTILvTuUuJogwC1F8yXYeZqGq-nZNCs0h8WPr45y5bjhKgu-y-d4nHBk66-K-TXDThCrjHOk4uHDF87-iZ_olq0LefVKzhQFiEIPpUSoIQRUsQ6YQCkAzNv5XHCvYrNEPXw6-TKnoipvlyqHuQ_aKtlJb-gk0eBeNY7sSZ710EZzo5A4ZhsMfwKdmmbmrkHNJ_G1So2Yf52niG3Y7ijCyk4113CTig8lpR0JFHvSXg8ctM5F8pcWPbntUzXcIPTUCVcX4UCdRXNvIvwQj3qbpFhSK4QYYfBzeLrlgBObcgWcHS22lav1cuisWGJHz3-cX4iBwzrPQmbZjoFY7Evanw4c2fD_RZRRh0ebK1trreDTG3qO5vkKUQm3UdirWxlQAdn2qLo8Oq-j5v-amu3PptxQp8x-1Kp_qrdwIk0crWxm7Ex3ba3rZM9TCo9ZbpY4VfL_pujrpyBgV7xTekvQzgBUlbTiTHcur-vQu5ecAhlsE1r4MQShejAmtQzGxTAPW61oQ2rMR_1I0MB17Kx5RlB9FtqdeDqFwIvqTMCi2A__JKpMU3v2t_s3CsDHtjnvllaZ4eytzEowNT9AphmW2zK9aSB8o9hLUjzZYR6M80x7UvPZt_d8u_zwkZ4YJAF6520kTEHw61aNzr5G8hF-FlBMbDO0F6fe3QYI4ykBxbN6agfQbI9ntzdqDKWOIijQxZGkW5h3bxH2erUQDQKDBSFB6LSwDX0WHKuZV8KiNSkOEAiasB-L4RYBvKSTPbu6GTONp4hVvkAJ_yyYmmljeB85aefF5iYCcxeGNCHUBqR5w6wzOcGBZQUF_ESWG3OpSl0POjzQ3XzAveOuTaQpZr1kAGMa4dwIehx-KOcpQwipIktTu8L_wRR1sOd6xD6iRwgwSDF2NvEoeW6nESMeCAJC-TtfvhlR0OP4tJxSNfCE--MaQWBviTKrYGHWjoE9tT1adRvmt0nH-X-tZI-ol1ro66JDmr0nXuMBa1Vz1qAHrnSPNE-Rem3DNForryu_Uv35cq33uYdHS_Gs3-1QyxD5fg4_10QIWOMZmGC5DRj3nqa6P6wonbSPFA9WP7l0UBH89s5lXSWp28SaP8jbJKcOu3UuDykq8H5GRv3WMVFreCqp74i7mWPYlU85t6LZRoN854B0s1EYWxZ1A0tLFir68HEtj70oehx3GJdPZ4rjf9lRBjTi_DgqQb2hLJYHCZ0jrJYZp1a8K9RmM3mqEzHH4rqDazYnW-AOq1b4_0oV__HDiudzKRBOiW7lhNFjrFWzw7zXqxcIHvKQOT4CwZQuIWGD86SWBkektHO5M4F7ef67quRwBiwcIo5rYYepOTCjdSvfJZ2VLAqWwJYSf_50QzVMmdls4mpziYeH2i0xgsaCdVs6qLGYK0a1KWKdJit0ymTVUXjb-2ahMoxnbCIbsIRhKFO8jX5sC97NENl9N5spftW0rFCIY6iS3PqgdrO1Pf-KejcL1WKcdRlZ4OTTzWn8lIAwsSqThhvXesTf_pzVlRgiO9WQQMyPEqsHwXHp_Gme_SznRC2UiejAl3Eig4ZAl18EWYAaxy9HV8CHnArQ5IwBhtuRN6JH9VXOgFbgJNO4f99-lXNHVRp7tAVk2DapwYp5gXlNye8kGwchGKTN3A3taujxo6FD8X9CXb8BZk8opY9sL-mPh7WZGYwRV_zxIsc_k0JK8v9r9Ms7GaOfB4vPAUZY6Eog1MsdpGIjXCe-bGLYEil2asvBPgJPXyI9ZpO3CJRJtRO_W5q8plMjHIxesUo85jmSm8kdsuAeXVw1mNI5F02tSDnLnsEjVAQVDw4JUqrWZGI1kKk_w70ppOWsNf-s-kGWgnV26N1_35JCoPvBWUP70sM5mRHsNRGmTwcDuwL_IsLDbHsiHgQUWDF4RtSNp7mkHzPs4tU2Hc99-woYJskADxczT5T7DNblSLfxrmv1WQk14e8OVacYrSSTWarS5KVIYxovsZ_2ed4VWhOGVsGOTwce3f_zyzLkrWG2gu8zkQtDRkyOBU8Frd9sDYsxYznIB4taxSd7m1vknDXF0tBEupWX2jsprz2cKrns_rtuLqQknHQFxMJVll0uaH2Cq1pj60d35G_qtsWGQbiQnSQvW_1vTQkVzq8-9CzHKzcU9wwXDc2-bWU-hgzUcfEatUPEQI-ZuuVYb5Z13wsii-ZfbvYLHKpd2oqoI7SVnZjd5FbHQcFXLOLLjumI1AcWrPj7Wj8T3RFfpOZ_jRSa3BYKp1JXJ9AmzsC; PALoginC2=4Fn0wDYFaLyBdUGAxIFtSHe62OL1WuaD_Egz5Jp0E0_cw4YnHPclk2QcAb_ITz67o3QQ14xitnjBHxQO4Oxrfc2snDK0RsD7i10JF73Dl9rOcRbhqF2YNaVbH8s_YEPZmeKUogs24vqCBizm1Y5uM9YofevJRR7r1GPgwxYD1NHm1lY-9mXOla8Z0apTTStfI-c1BoefhgX_AQKvNwj4y_gF1jGio9vhpaoPweqjb530hEJ4AFd1j7FixNRDCy4laYEHOa1fKIS8cd34nPDzO2BDleJ3tQ4eBGal17A--JtIToGnwNqfm-Ppv9QMxPwVSCTJBhSJmy57fBvVU8W7YCgEDngPxUMD00ULu3a21Lq83Gw5Zd6uj_MSFD1z_ChYThZBcS2i9kviuDsFajqVejxIzI2ABcx9BrJBQdue8h-mrqD_xXMN5Rl0G9uYsdG3fmBNUREyQckTpHzUmmc7_6I1AvPc8hfXZ_-eEry6683Mn5q2TInh1PIo6nvQmvr1myZhJ5UGjcsIzdr-mLp5R6EwjBQHQyRAihhaCga1bDW0UKBMJfp_U0oAVrbrFCWI6GjkZEAU1imLkmJoyTgwaWQ61cX67AYwZBNkp-vJZheIZqTA1-nTbUmvw4ktxPE5XENHYeBEIbjTaPiGG8mhlGTXG2sEmsy2L05VxPEiGVkLZNfDHSvZbXXQ8zI9gICbIJWTjC_C4GRR8wjdAL_c2K-DjxTliiSSlyUEkYdDFKYBtGiJSU3K0PzgrSYQD00kvqeCq0ktgbuYLe_eCZbl_Z_uCQIIYPnGQzGve2bFwl9TpKAPwjiwiC_qTEt7tcOc6pVbrVE3o0BUr07FIcSR2ZJPEuOkV_knO1cshZvfBpICkR87nbGYDnrhwmZULl49yJhPzBQDm2yRGrdUSkRUdQMBZNbjktTtJp__Tb7H2gCf-lLvHd9b3eHa5e9-Eum3lYuNgyrwtA9lbjjS1FngEoPgNOtOeMipo-p1rhGM1_xSp4-xboc5oEt98IkRNb3Nz7NMrCnB0MVo7uIqz87a4Cli2Vhmmh8DnL6sBUc-TFZGeWnX2ACR1fq_ol4RpgaK21p3A-0t5C8CIPWQEwI6yUMRG-KekxTFXAE0GrJ8zA-edBGprQzTLlws9YXugFzegEc5QgRZWT-NOnBcg9OQO6Ie48qepyGFvckLbUiyFkEyZ07Fi4QjdZPw9ulqRLrf5_Dat55YWAPyx6s8y0r0NDrzBrHkxGsGAPlhdF3pTrj7ghLi4R5UWu3TvM8g2m6YuvkmxTuG45WgL4kRib0ywYoOeS1NgvJdASbmeN2dMHEI9SfRE32tvBAMTy-usWQjYBiKcA_4fvNXNIxQ8KzvCydhoBwMMDT8slKUczyQgnqmsZtz7uhs8uiky9TCVik0ncjIJa-vP2MJNLiZ9TEKWHTwYNYp43aj0M-cnJ9CUfGauKvprEJBr-xDYpAH5MUaDpTjOSvAXAoGFo_DGCAcswggHHAgEBMIGjMIGLMQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMRUwEwYDVQQLEwxNaWNyb3NvZnQgSVQxHjAcBgNVBAMTFU1pY3Jvc29mdCBJVCBUTFMgQ0EgMgITIAAMuLryBJEcSnyNGAAAAAy4ujAJBgUrDgMCGgUAMA0GCSqGSIb3DQEBAQUABIIBAIvl38w6mQPugIWgDqb1BSGzmTGi6wM8BumsaKYNsDgH4RhNux-56MoSK-XVwGtnj1TVT0w1vI2AwIzL-6Vj95jTrGtsMoWHFw6KneQwQeZwQvEIOaS-xS_il2dXTg1jLJzjNPBnui6AFuJPqXOl1awAiY1BesTQ6EY5Ca9VZkWv_ygf_gkUrMFlr1IdqPAer66PzBNMGcw63bP0Y9B-mSrlC4lM8YJdXVMsx-KHvbClt1vqxVcho6cE-RIlqTcmuQglzyWcLBAngRQsCW4Ai1IiaBv2guO2vQxnUIhD5tS61lgpRN-TOH-0looLc4ch9hkzJ2MBRwv62w6QHhQCW10; POWERAPPS.AUTHSTARTPAGE=%2Fdataintegration%2Fprojects; POWERAPPS.SAVEDSEARCH=
Content-Length: 0
            */


        public const string BASE_URL = "https://integ-atm-prod-eu.rsu.powerapps.com";
        public const string PROJECT_SUMMERIES = "/IntegratorApp/ProjectManagementService/api/ProjectSummary";
        public const string PROJECT_DETAILS = "/IntegratorApp/ProjectManagementService/api/Project/";
        public static string ListProjects(string token)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(BASE_URL + PROJECT_SUMMERIES),
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = AsyncUtil.RunSync<HttpResponseMessage>(() => client.SendAsync(request));


            if (response.IsSuccessStatusCode)
                return AsyncUtil.RunSync<string>(() => response.Content.ReadAsStringAsync());
            throw new Exception("error code is " + response.StatusCode + " reason phrase " + response.ReasonPhrase);
        }


        public static string ListProjectTasks(string token, string projectName)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(BASE_URL + PROJECT_DETAILS + projectName),
                Method = HttpMethod.Get,
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = AsyncUtil.RunSync<HttpResponseMessage>(() => client.SendAsync(request));
            if (response.IsSuccessStatusCode)
                return AsyncUtil.RunSync<string>(() => response.Content.ReadAsStringAsync());
            throw new Exception("error code is " + response.StatusCode + " reason phrase " + response.ReasonPhrase);
        }
        

        public static string PutProject(string token, string projectName, string project)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);

            var content = new StringContent(project, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(BASE_URL + PROJECT_DETAILS + projectName),
                Method = HttpMethod.Put,
                Content = content
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = AsyncUtil.RunSync<HttpResponseMessage>(() => client.SendAsync(request));
            if (response.IsSuccessStatusCode)
                return AsyncUtil.RunSync<string>(() => response.Content.ReadAsStringAsync());
            throw new Exception("error code is " + response.StatusCode + " reason phrase " + response.ReasonPhrase);
        }
    }
}
