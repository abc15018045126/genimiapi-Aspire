using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // Using Newtonsoft for consistency with ChatGPT page

namespace AspireApp2.Web.Components.Pages
{
    // Using a base class approach for cleaner separation (optional)
    public class PostTestBase : ComponentBase
    {
        [Inject]
        protected HttpClient Http { get; set; } = default!;

        // Use the same defaults as ChatGPT page for testing consistency
        private static readonly string testApiUrl = "https://gemini.abc15018045126.ip-ddns.com/v1/chat/completions"; 
        private static readonly string testApiKey = "AIzaSyDmGfx7r-MP8XglVrGkcG51JtTsqSH31uI"; // Use the same key for auth test

        protected string testResult = "尚未开始测试...";
        protected bool isTesting = false; // This field might not be needed anymore for the simple test

        // --- New method for simple test ---
        protected void ShowHelloWorld()
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: ShowHelloWorld entered at {DateTime.Now}"); // Keep debug line
            Console.WriteLine($"DEBUG: ShowHelloWorld entered via Console at {DateTime.Now}"); // Keep console line
            testResult = "hello world";
            StateHasChanged(); // Ensure the UI updates to show the new message
        }
        // --- End of new method ---

        protected async Task SendTestPostAsync()
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG: SendTestPostAsync entered at {DateTime.Now}"); // Add this line
            Console.WriteLine($"DEBUG: SendTestPostAsync entered via Console at {DateTime.Now}"); // Also add this for server console

            // --- Original code below ---
            isTesting = true;
            testResult = "正在发送请求...";
            StateHasChanged();

            try
            {
                // 1. 创建一个简单的请求体 (模仿 Gemini API 的结构，但内容简化)
                var requestBody = new
                {
                    model = "gemini-test-model", // Use a distinct model name for test if needed
                    messages = new[] {
                        new { role = "user", content = "This is a test message." }
                    }
                };
                string jsonContent = JsonConvert.SerializeObject(requestBody);
                testResult += $"\n请求体:\n{jsonContent}\n";

                // 2. 准备 HttpRequestMessage
                testResult += $"\n准备发送到: {testApiUrl}\n";
                using var request = new HttpRequestMessage(HttpMethod.Post, testApiUrl);
                
                // 3. 添加认证头 (如果 API 需要)
                if (!string.IsNullOrEmpty(testApiKey))
                {
                     request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", testApiKey);
                     testResult += "已添加 Authorization Header.\n";
                }
                else
                {
                     testResult += "警告: API Key 为空，未添加 Authorization Header.\n";
                }

                // 4. 设置请求内容
                request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                testResult += "请求内容已设置 (application/json).\n";

                // 5. 发送请求
                testResult += ">>> 正在调用 Http.SendAsync...\n";
                StateHasChanged(); // Update UI before the await

                HttpResponseMessage response = await Http.SendAsync(request);

                testResult += $"<<< Http.SendAsync 调用完成. 状态码: {response.StatusCode}\n";

                // 6. 读取响应内容
                string responseContent = await response.Content.ReadAsStringAsync();

                testResult += $"响应状态: {response.StatusCode}\n";
                testResult += $"响应内容:\n{responseContent}\n";

                if (!response.IsSuccessStatusCode)
                {
                    testResult += "\n测试失败 (非成功状态码).\n";
                    // Optional: throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }
                else
                {
                     testResult += "\n测试成功 (收到成功状态码).\n";
                }

            }
            catch (HttpRequestException httpEx)
            {
                testResult += $"\n发生 HttpRequestException:\n{httpEx.Message}\n";
                if (httpEx.StatusCode.HasValue)
                {
                    testResult += $"状态码: {httpEx.StatusCode.Value}\n";
                }
                 testResult += $"堆栈跟踪:\n{httpEx.StackTrace}\n";
            }
            catch (Exception ex)
            {
                testResult += $"\n发生未知错误:\n{ex.Message}\n堆栈跟踪:\n{ex.StackTrace}\n";
                 // Add Debug.WriteLine here too if needed
                 System.Diagnostics.Debug.WriteLine($"DEBUG: Exception in SendTestPostAsync: {ex.Message}");
            }
            finally
            {
                isTesting = false;
                StateHasChanged();
            }
        }
    }
}