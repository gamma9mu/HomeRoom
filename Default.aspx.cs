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
        visual = 0;
        aural = 0;
        tactile = 0;

        foreach (Control question in surveyContent.Controls)
        {
            foreach (Control level2 in question.Controls)
            {
                switch (((RadioButtonList)level2).SelectedValue) // thanks to http://forums.asp.net/t/1007830.aspx
                {
                    case "a":
                        visual++;
                        break;
                    case "b":
                        aural++;
                        break;
                    case "c":
                        tactile++;
                        break;
                }
            }
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
        results.Text = "Submission successful!" + visual + aural + tactile;
    }
}
