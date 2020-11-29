package com.example.callouskitchenandroid

import android.app.NotificationChannel
import android.app.NotificationManager
import android.content.BroadcastReceiver
import android.content.Context
import android.content.Intent
import android.graphics.Color
import android.os.Build
import androidx.core.content.ContextCompat.getSystemService
import android.app.Notification
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.ViewGroup
import android.widget.Button
import android.widget.Toast
import androidx.core.app.NotificationCompat
import com.android.volley.Response
import com.google.android.material.floatingactionbutton.FloatingActionButton
import kotlinx.android.synthetic.main.activity_inventory.*
import kotlinx.android.synthetic.main.activity_kitchen_list.*
import org.json.JSONObject
import android.app.PendingIntent
import android.graphics.BitmapFactory
import android.widget.RemoteViews
import androidx.core.app.NotificationManagerCompat
import androidx.core.content.ContextCompat
import java.time.LocalDate
import java.time.LocalDateTime

class NotificationReceiver : BroadcastReceiver() {
    //declaring variables
    override fun onReceive(context: Context, intent: Intent?) {
        Util.scheduleJob(context);
    }
}
