namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdSpLoadAsync(short sp_no = 1)
    {
        return await Task.FromResult(RdSpLoad(sp_no));
    }

    public dynamic RdSpLoad(short sp_no = 1)
    {
        var serial_spindle = new Focas.ODBSPN();

        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_rdspload(_handle, sp_no, serial_spindle);
        });

        var nr = new
        {
            @null = false,
            method = "cnc_rdspload",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/position/cnc_rdspload",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rdspload = new {sp_no}},
            response = new {cnc_rdspload = new {serial_spindle}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}