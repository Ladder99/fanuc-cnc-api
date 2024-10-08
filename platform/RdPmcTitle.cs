namespace l99.fanuc;

public partial class Platform
{
    public async Task<dynamic> RdPmcTitleAsync()
    {
        return await Task.FromResult(RdPmcTitle());
    }

    public dynamic RdPmcTitle()
    {
        dynamic title = new Focas.ODBPMCTITLE();

        var ndr = _nativeDispatch(() => { return (Focas.focas_ret) Focas.pmc_rdpmctitle(_handle, title); });

        var nr = new
        {
            @null = false,
            method = "pmc_rdpmctitle",
            invocationMs = ndr.ElapsedMilliseconds,
            doc = $"{_docBasePath}/pmc/pmc_rdpmctitle",
            success = ndr.RC == Focas.EW_OK,
            rc = ndr.RC,
            request = new {pmc_rdpmctitle = new { }},
            response = new {pmc_rdpmctitle = new {title}}
        };

        _logger.Trace($"Platform invocation result:\n{JObject.FromObject(nr)}");

        return nr;
    }
}