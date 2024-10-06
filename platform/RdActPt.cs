namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdActPtAsync()
    {
        return await Task.FromResult(RdActPt());
    }

    public dynamic RdActPt()
    {
        var prog_no = 0;
        var blk_no = 0;

        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_rdactpt(_handle, out prog_no, out blk_no);
        });

        var nr = new
        {
            @null = false,
            method = "cnc_rdactpt",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/program/cnc_rdactpt",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rdactpt = new { }},
            response = new {cnc_rdactpt = new {prog_no, blk_no}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}