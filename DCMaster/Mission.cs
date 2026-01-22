using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Wump
{
    class Mission
    {
        public Mission()
        {

        }

        private void bttnStartMission_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            labirynth lab = getLab(cmbLabirynths.SelectedItem.ToString());
            worker[] wk = getWorkers(lab);
            int numOfWorkers = wk.Length;
            int numberOfWumpus = lab.numOfWumpusTrapsGold;
            this.Cursor = Cursors.WaitCursor;
            pBar.Maximum = numOfWorkers;
            for (int i = 0; i < numOfWorkers; i++)
            {
                wk[i].StartLocation = getStartLocation(rand, lab);
                int row = Convert.ToInt16(wk[i].StartLocation.Split(',')[1]);
                int col = Convert.ToInt16(wk[i].StartLocation.Split(',')[0]);
                dbTools dbT = new dbTools(connsb, wk[i], lab);
                logBookWriter(wk[i].ID, lab.LabId, wk[i].StartLocation);
                while (wk[i].Live)
                {
                    wk[i].moveNext();
                    dbT.update_worker_energy(wk[i].Energy, wk[i].ID);
                    if (wk[i].Gotcha)
                    {
                        wk[i].goBack2Start();
                        dbT.update_worker_energy(wk[i].Energy, wk[i].ID);
                        break;
                    }
                }
                if (wk[i].Live == false)
                {
                    dbT.setupWorkersLive(wk[i].ID);
                }
                dbT.saveWorkerPath2DB();
                pBar.PerformStep();
            }
            dbTools deleteDeadWorkers = new dbTools(connsb);    // ************** DELETE dead workers ******************************
            deleteDeadWorkers.deleteDeadWorkers();     // ************** DELETE dead workers ******************************
            this.Cursor = Cursors.Default;
            pBar.Value = 0; ;
            MessageBox.Show("This run has been terminated", "Run terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
