using System;
using HomeRoom;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public int visual, aural, tactile;
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void quantifySurvey()
    {
        int count = 0;
        visual = 0;
        aural = 0;
        tactile = 0;
        

        foreach (Control control in surveyContent.Controls)
        {
            if (control is RadioButtonList)
            {
                switch (((RadioButtonList)control).SelectedValue)
                {
                    case "a":
                        count++;
                        visual++;
                        break;
                    case "b":
                        count++;
                        aural++;
                        break;
                    case "c":
                        count++;
                        tactile++;
                        break;
                }
            }
        }

        if (count > 0)
        {
            visual = visual * 100 / count;
            aural = aural * 100 / count;
            tactile = tactile * 100 / count;
        }
        else
        {
            visual = 33;
            aural = 33;
            tactile = 33;
        }
    }

    public void submitQuery(object sender, EventArgs e)
    {
        quantifySurvey();

        StudentInformation student = new StudentInformation(visual, aural, tactile);

        Controller controller = Controller.getInstance();
        Request request = new Request(student, query.Text, Int32.Parse(speed.SelectedValue));
        controller.addRequest(request);

        query.Text = "";
        results.Text = "Submission successful!";
    }
}
