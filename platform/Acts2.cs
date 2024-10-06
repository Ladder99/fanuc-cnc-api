namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> Acts2Async(short sp_no = -1)
    {
        return await Task.FromResult(Acts2(sp_no));
    }

    public dynamic Acts2(short sp_no = -1)
    {
        var actualspindle = new Focas.ODBACT2();

        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.cnc_acts2(_handle, sp_no, actualspindle); });

        var nr = new
        {
            @null = false,
            method = "cnc_acts2",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/position/cnc_acts2",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_acts2 = new {sp_no}},
            response = new {cnc_acts2 = new {actualspindle}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}