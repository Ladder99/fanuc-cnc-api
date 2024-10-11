namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> DncEnd2Async(short result = (short)Focas.focas_ret.DNC_NORMAL)
    {
        return await Task.FromResult(DncEnd2(result));
    }

    public dynamic DncEnd2(short result)
    {
        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_dncend2(_handle, result); });

        var nr = new
        {
            @null = false,
            method = "cnc_dncend2",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_dncend2",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_dncend2 = new {result}},
            response = new {cnc_dncend2 = new { }}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}