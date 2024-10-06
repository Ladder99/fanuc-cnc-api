namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdDynamic2Async(short axis = 1, short length = 44, int ODBDY2_type = 2)
    {
        return await Task.FromResult(RdDynamic2(axis, length, ODBDY2_type));
    }

    public dynamic RdDynamic2(short axis = 1, short length = 44, int ODBDY2_type = 2)
    {
        dynamic rddynamic = new object();

        switch (ODBDY2_type)
        {
            case 1:
                rddynamic = new Focas.ODBDY2_1();
                break;
            case 2:
                rddynamic = new Focas.ODBDY2_2();
                break;
        }

        //length = (short) Marshal.SizeOf(rddynamic);

        var ndr = _nativeDispatch(() =>
        {
            return (Focas.focas_ret) Focas.cnc_rddynamic2(_handle, axis, length, rddynamic);
        });

        var nr = new
        {
            @null = false,
            method = "cnc_rddynamic2",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/position/cnc_rddynamic2",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {cnc_rddynamic2 = new {axis, length}},
            response = new {cnc_rddynamic2 = new {rddynamic}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}