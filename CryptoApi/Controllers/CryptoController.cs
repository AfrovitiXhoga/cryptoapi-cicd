using Microsoft.AspNetCore.Mvc;

namespace CryptoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CryptoController : ControllerBase
{
    [HttpPost("encrypt")]
    public ActionResult<CryptoResponse> Encrypt([FromBody] CryptoRequest req)
    {
        var result = CaesarShift(req.Text, req.Shift);
        return Ok(new CryptoResponse(result));
    }

    [HttpPost("decrypt")]
    public ActionResult<CryptoResponse> Decrypt([FromBody] CryptoRequest req)
    {
        var result = CaesarShift(req.Text, -req.Shift);
        return Ok(new CryptoResponse(result));
    }

    [HttpGet("health")]
    public IActionResult Health() => Ok("OK");

    private static string CaesarShift(string input, int shift)
    {
        if (string.IsNullOrEmpty(input)) return input;

        shift %= 26;
        var chars = input.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            char c = chars[i];
            if (char.IsLetter(c))
            {
                char offset = char.IsUpper(c) ? 'A' : 'a';
                int alphaIndex = c - offset;
                int shifted = (alphaIndex + shift) % 26;
                if (shifted < 0) shifted += 26;
                chars[i] = (char)(offset + shifted);
            }
        }

        return new string(chars);
    }
}

public sealed record CryptoRequest(string Text, int Shift = 3);
public sealed record CryptoResponse(string Result);
