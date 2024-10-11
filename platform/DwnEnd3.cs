namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> DwnEnd3Async()
    {
        return await Task.FromResult(DwnEndt3());
    }

    public dynamic DwnEndt3()
    {
        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_upend3(_handle); });

        var nr = new
        {
            @null = false,
            method = "cnc_dwnend3",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_dwnend3",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_dwnend3 = new {}},
            response = new {cnc_dwnend3 = new { }}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}