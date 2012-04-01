using System;
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

        Controller controller = Controller.getInstance();
        Request request = new Request(student, query.Text, Int32.Parse(speed.SelectedValue));
        controller.addRequest(request);

        query.Text = "";
        results.Text = "Submission successful!";
    }
}
