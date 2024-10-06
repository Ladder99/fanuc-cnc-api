namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdEtherInfoAsync()
    {
        return await Task.FromResult(RdEtherInfo());
    }

    public dynamic RdEtherInfo()
    {
        short type = 0, device = 0;

        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_rdetherinfo(_handle, out type, out device);
        });

        var nr = new
        {
            @null = false,
            method = "cnc_rdetherinfo",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/misc/cnc_rdetherinfo",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rdetherinfo = new { }},
            response = new {cnc_rdetherinfo = new {type, device}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}