using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void submitQuery(object sender, EventArgs e)
    {
        HomeRoom.StudentInformation student =
            new HomeRoom.StudentInformation(
                Int32.Parse(visual.Text),
                Int32.Parse(aural.Text),
                Int32.Parse(tactile.Text));

        queryform.Visible = false;
        results.Text = "Submission successful";
    }
}