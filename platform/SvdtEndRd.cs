namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> SvdtEndRdAsync()
    {
        return await Task.FromResult(SvdtEndRd());
    }

    public dynamic SvdtEndRd()
    {
        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_svdtendrd(_handle); });

        var nr = new
        {
            @null = false,
            method = "cnc_svdtendrd",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/servo/cnc_svdtendrd",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_svdtendrd = new { }},
            response = new {cnc_svdtendrd = new { }}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}