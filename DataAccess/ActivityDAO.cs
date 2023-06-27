/*using BusinessObjects.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ActivityDAO
    {
        private static ActivityDAO instance = null;
        private static readonly object instanceLock = new object();
        private ActivityDAO() { }

        public static ActivityDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ActivityDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            List<Activity> activities = new List<Activity>(); 
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    activities = context.Activities.Include(p=>p.School).Include(p=>p.Location).Include(p=>p.CreatedByNavigation).ToList();
                }            
            }catch(Exception ex)
            {
                throw new Exception("Error at GetAllActivities: " + ex.Message);
            }
            return activities;
        }

        public IEnumerable<Activity> GetActivitiesForStudents()
        {
            List<Activity> activities = new List<Activity>();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    foreach(var activity in context.ActivityVisitorInvitations)
                    {
                        if (activity.InvitationCode == null)
                        {
                            activities.Add(GetActivityByID(activity.Id));
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetAllActivities: " + ex.Message);
            }
            return activities;
        }

        public IEnumerable<Activity> GetActivitiesForVisitors()
        {
            List<Activity> activities = new List<Activity>();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    foreach (var activity in context.ActivityVisitorInvitations)
                    {
                        if (activity.InvitationCode != null)
                        {
                            activities.Add(GetActivityByID(activity.Id));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at GetAllActivities: " + ex.Message);
            }
            return activities;
        }

        public Activity GetActivityByID(Guid id)
        {
            Activity activity = new Activity();
            try
            {
                using (var context = new FPTHCMAdventuresDBContext())
                {
                    activity = context.Activities.SingleOrDefault(a => a.Id == id);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error at GetActivityByID: " + ex.Message);
            }
            return activity;
        }

        public Activity CreateActivity(Activity activity,int bonuspoint, Boolean hasInviteCode) 
        {
            try
            {
                ActivityVisitorInvitation activityVisitorInvitation = new ActivityVisitorInvitation
                {
                    Id = Guid.NewGuid(),
                    UserId = activity.CreatedBy,
                    BonusPoint = bonuspoint,
                    IsActive = true
                };
                if (hasInviteCode)
                {
                    activityVisitorInvitation.InvitationCode = GenerateCode();
                }
                var context = new FPTHCMAdventuresDBContext();
                activity.Id = Guid.NewGuid();

                context.Activities.Add(activity);
                context.ActivityVisitorInvitations.Add(activityVisitorInvitation);
            }
            catch(Exception ex)
            {
                throw new Exception("Error at CreateActivity: " + ex.Message);
            }
            return activity;
        }

        public Activity UpdateActivity(Activity activity)
        {
            try
            {
                var context = new FPTHCMAdventuresDBContext();
                context.Activities.Update(activity);
            }
            catch(Exception ex)
            {
                throw new Exception("Error at UpdateActivity: " + ex.Message);
            }
            return activity;
        }

        private string GenerateCode()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray()).Substring(0, 6);
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");
            GuidString = GuidString.Replace("'\'", "");
            return GuidString;
        }
    }
}
*/