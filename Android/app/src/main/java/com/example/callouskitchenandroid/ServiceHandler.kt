package com.example.callouskitchenandroid

import android.content.Context
import android.widget.Toast
import com.android.volley.Request
import com.android.volley.RequestQueue.RequestFinishedListener
import com.android.volley.Response
import com.android.volley.toolbox.JsonObjectRequest
import com.android.volley.toolbox.StringRequest
import com.android.volley.toolbox.Volley
import com.google.gson.Gson
import com.google.gson.GsonBuilder
import org.json.JSONObject
import java.time.Instant
import java.time.LocalDate
import java.time.LocalDateTime
import java.time.ZoneOffset
import java.time.format.DateTimeFormatter
import java.util.*
import kotlin.collections.HashMap


class ServiceHandler {
    //this is basically a singleton
    companion object Static {
        //I don't know where I should put variables that I want to be accessible to the whole app
        //So I'm putting them here for now
        //The user's id
        var userId = -1
        var lastKitchenId = -1
        val baseUrl = "http://10.0.2.2:59869"
        //The name of the service with extension, the name of the method, the parameters where the string
        //is the parameter name and any is the value, for context use this and use response as your listener
        //I really tried to eliminate the listener and return the response as a string, but it's not possible
        fun callService(service : String, method : String, parameters : HashMap<String,Any?>, context: Context, response : Response.Listener<JSONObject>)
        {
            val queue = Volley.newRequestQueue(context)
            val url = "$baseUrl/$service/$method"

            var jsonObject = JSONObject()
            for ((key, value) in parameters){
                jsonObject.put(key,value)
            }
            println("the json request is $jsonObject")
            val request = JsonObjectRequest(Request.Method.POST, url, jsonObject,
                response,
                Response.ErrorListener { println("request failed") })
            queue.add(request)
        }
        fun callAccountService(method : String, parameters : HashMap<String,Any?>, context: Context, response : Response.Listener<JSONObject>)
        {
            callService("AccountService.svc",method,parameters,context, response)
        }

        fun serializeDate(date : LocalDate) : String{

            var dateStart = date.atStartOfDay()
            //Adds 4 hours in seconds to make up for timezone differences
            //I don't know what the rest of the numbers and dashes mean but they might be important
            //The first three zeros are actually important though as they change it milliseconds
            return "/Date("+(dateStart.toEpochSecond(ZoneOffset.UTC)+14400000).toString()+"000-00-00T00:00:00.0-00:00)/"
        }

        //this is the hackiest thing in the worl
        fun deSerializeDate(date : String) : LocalDate?{
            if (date.isNullOrEmpty() || date == "null"){
                return null
            }
            //get the miliseconds since 1970 from the string
            var epoch = date.substring(6,date.length-10)
            //convert that to a date
            var wrongDate = LocalDateTime.ofEpochSecond(epoch.toLong(),0,ZoneOffset.UTC).toLocalDate()
            //This offset needs to happen foor some reason
            return wrongDate.minusDays(166)

        }
    }

}