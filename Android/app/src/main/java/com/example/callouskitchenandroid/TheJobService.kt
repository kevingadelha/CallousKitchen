package com.example.callouskitchenandroid

import android.app.job.JobParameters
import android.app.job.JobService
import android.content.Intent
import com.example.callouskitchenandroid.Util.scheduleJob


/**
 * JobService to be scheduled by the JobScheduler.
 * start another service
 */
class TheJobService : JobService() {
    override fun onStartJob(params: JobParameters): Boolean {
        val service = Intent(applicationContext, NotificationService::class.java)
        applicationContext.startService(service)
        scheduleJob(applicationContext) // reschedule the job
        return true
    }

    override fun onStopJob(params: JobParameters): Boolean {
        return true
    }

    companion object {
        private const val TAG = "SyncService"
    }
}