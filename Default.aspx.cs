using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Collections;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (Page.IsPostBack)
        {
            results.Visible = true;
        }
        else
        {
            results.Visible = false;
        }
    }

    public void submitSurvey(object sender, EventArgs e) {
        StudentInformation student = new StudentInformation(Int32.Parse(visualpercent.Text), Int32.Parse(auralpercent.Text), Int32.Parse(tactilepercent.Text));
        results.Text = "submitted";
    }
}

public class StudentInformation
{
    int visualpercent, auralpercent, tactilepercent;

    public StudentInformation(int vp, int ap, int tp)
    {
        visualpercent = vp;
        auralpercent = ap;
        tactilepercent = tp;
    }
}
