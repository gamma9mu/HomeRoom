using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using HomeRoom;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void submitQuery(object sender, EventArgs e)
    {
        StudentInformation student = new StudentInformation(
                Int32.Parse(visual.Text),
                Int32.Parse(aural.Text),
                Int32.Parse(tactile.Text));

        queryform.Visible = false;

        Controller controller = Controller.getInstance();
        Request request = new Request(student, query.Text, 1500000);
        controller.addRequest(request);
    }
}
