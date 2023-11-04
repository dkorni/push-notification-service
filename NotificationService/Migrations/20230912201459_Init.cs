﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationService.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");
            migrationBuilder.Sql(
                "# By: Ron Cordell - roncordell\n#  I didn't see this anywhere, so I thought I'd post it here. This is the script from Quartz to create the tables in a MySQL database, modified to use INNODB instead of MYISAM.\n\n\n# make sure you have UTF-8 collaction for best .NET interoperability\n# CREATE DATABASE quartznet CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;\n\nDROP TABLE IF EXISTS QRTZ_FIRED_TRIGGERS;\nDROP TABLE IF EXISTS QRTZ_PAUSED_TRIGGER_GRPS;\nDROP TABLE IF EXISTS QRTZ_SCHEDULER_STATE;\nDROP TABLE IF EXISTS QRTZ_LOCKS;\nDROP TABLE IF EXISTS QRTZ_SIMPLE_TRIGGERS;\nDROP TABLE IF EXISTS QRTZ_SIMPROP_TRIGGERS;\nDROP TABLE IF EXISTS QRTZ_CRON_TRIGGERS;\nDROP TABLE IF EXISTS QRTZ_BLOB_TRIGGERS;\nDROP TABLE IF EXISTS QRTZ_TRIGGERS;\nDROP TABLE IF EXISTS QRTZ_JOB_DETAILS;\nDROP TABLE IF EXISTS QRTZ_CALENDARS;\n\nCREATE TABLE QRTZ_JOB_DETAILS(\nSCHED_NAME VARCHAR(120) NOT NULL,\nJOB_NAME VARCHAR(200) NOT NULL,\nJOB_GROUP VARCHAR(200) NOT NULL,\nDESCRIPTION VARCHAR(250) NULL,\nJOB_CLASS_NAME VARCHAR(250) NOT NULL,\nIS_DURABLE BOOLEAN NOT NULL,\nIS_NONCONCURRENT BOOLEAN NOT NULL,\nIS_UPDATE_DATA BOOLEAN NOT NULL,\nREQUESTS_RECOVERY BOOLEAN NOT NULL,\nJOB_DATA BLOB NULL,\nPRIMARY KEY (SCHED_NAME,JOB_NAME,JOB_GROUP))\nENGINE=InnoDB;\n\nCREATE TABLE QRTZ_TRIGGERS (\nSCHED_NAME VARCHAR(120) NOT NULL,\nTRIGGER_NAME VARCHAR(200) NOT NULL,\nTRIGGER_GROUP VARCHAR(200) NOT NULL,\nJOB_NAME VARCHAR(200) NOT NULL,\nJOB_GROUP VARCHAR(200) NOT NULL,\nDESCRIPTION VARCHAR(250) NULL,\nNEXT_FIRE_TIME BIGINT(19) NULL,\nPREV_FIRE_TIME BIGINT(19) NULL,\nPRIORITY INTEGER NULL,\nTRIGGER_STATE VARCHAR(16) NOT NULL,\nTRIGGER_TYPE VARCHAR(8) NOT NULL,\nSTART_TIME BIGINT(19) NOT NULL,\nEND_TIME BIGINT(19) NULL,\nCALENDAR_NAME VARCHAR(200) NULL,\nMISFIRE_INSTR SMALLINT(2) NULL,\nJOB_DATA BLOB NULL,\nPRIMARY KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP),\nFOREIGN KEY (SCHED_NAME,JOB_NAME,JOB_GROUP)\nREFERENCES QRTZ_JOB_DETAILS(SCHED_NAME,JOB_NAME,JOB_GROUP))\nENGINE=InnoDB;\n\nCREATE TABLE QRTZ_SIMPLE_TRIGGERS (\nSCHED_NAME VARCHAR(120) NOT NULL,\nTRIGGER_NAME VARCHAR(200) NOT NULL,\nTRIGGER_GROUP VARCHAR(200) NOT NULL,\nREPEAT_COUNT BIGINT(7) NOT NULL,\nREPEAT_INTERVAL BIGINT(12) NOT NULL,\nTIMES_TRIGGERED BIGINT(10) NOT NULL,\nPRIMARY KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP),\nFOREIGN KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP)\nREFERENCES QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP))\nENGINE=InnoDB;\n\nCREATE TABLE QRTZ_CRON_TRIGGERS (\nSCHED_NAME VARCHAR(120) NOT NULL,\nTRIGGER_NAME VARCHAR(200) NOT NULL,\nTRIGGER_GROUP VARCHAR(200) NOT NULL,\nCRON_EXPRESSION VARCHAR(120) NOT NULL,\nTIME_ZONE_ID VARCHAR(80),\nPRIMARY KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP),\nFOREIGN KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP)\nREFERENCES QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP))\nENGINE=InnoDB;\n\nCREATE TABLE QRTZ_SIMPROP_TRIGGERS\n  (          \n    SCHED_NAME VARCHAR(120) NOT NULL,\n    TRIGGER_NAME VARCHAR(200) NOT NULL,\n    TRIGGER_GROUP VARCHAR(200) NOT NULL,\n    STR_PROP_1 VARCHAR(512) NULL,\n    STR_PROP_2 VARCHAR(512) NULL,\n    STR_PROP_3 VARCHAR(512) NULL,\n    INT_PROP_1 INT NULL,\n    INT_PROP_2 INT NULL,\n    LONG_PROP_1 BIGINT NULL,\n    LONG_PROP_2 BIGINT NULL,\n    DEC_PROP_1 NUMERIC(13,4) NULL,\n    DEC_PROP_2 NUMERIC(13,4) NULL,\n    BOOL_PROP_1 BOOLEAN NULL,\n    BOOL_PROP_2 BOOLEAN NULL,\n    TIME_ZONE_ID VARCHAR(80) NULL,\n    PRIMARY KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP),\n    FOREIGN KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP) \n    REFERENCES QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP))\nENGINE=InnoDB;\n\nCREATE TABLE QRTZ_BLOB_TRIGGERS (\nSCHED_NAME VARCHAR(120) NOT NULL,\nTRIGGER_NAME VARCHAR(200) NOT NULL,\nTRIGGER_GROUP VARCHAR(200) NOT NULL,\nBLOB_DATA BLOB NULL,\nPRIMARY KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP),\nINDEX (SCHED_NAME,TRIGGER_NAME, TRIGGER_GROUP),\nFOREIGN KEY (SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP)\nREFERENCES QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP))\nENGINE=InnoDB;\n\nCREATE TABLE QRTZ_CALENDARS (\nSCHED_NAME VARCHAR(120) NOT NULL,\nCALENDAR_NAME VARCHAR(200) NOT NULL,\nCALENDAR BLOB NOT NULL,\nPRIMARY KEY (SCHED_NAME,CALENDAR_NAME))\nENGINE=InnoDB;\n\nCREATE TABLE QRTZ_PAUSED_TRIGGER_GRPS (\nSCHED_NAME VARCHAR(120) NOT NULL,\nTRIGGER_GROUP VARCHAR(200) NOT NULL,\nPRIMARY KEY (SCHED_NAME,TRIGGER_GROUP))\nENGINE=InnoDB;\n\nCREATE TABLE QRTZ_FIRED_TRIGGERS (\nSCHED_NAME VARCHAR(120) NOT NULL,\nENTRY_ID VARCHAR(140) NOT NULL,\nTRIGGER_NAME VARCHAR(200) NOT NULL,\nTRIGGER_GROUP VARCHAR(200) NOT NULL,\nINSTANCE_NAME VARCHAR(200) NOT NULL,\nFIRED_TIME BIGINT(19) NOT NULL,\nSCHED_TIME BIGINT(19) NOT NULL,\nPRIORITY INTEGER NOT NULL,\nSTATE VARCHAR(16) NOT NULL,\nJOB_NAME VARCHAR(200) NULL,\nJOB_GROUP VARCHAR(200) NULL,\nIS_NONCONCURRENT BOOLEAN NULL,\nREQUESTS_RECOVERY BOOLEAN NULL,\nPRIMARY KEY (SCHED_NAME,ENTRY_ID))\nENGINE=InnoDB;\n\nCREATE TABLE QRTZ_SCHEDULER_STATE (\nSCHED_NAME VARCHAR(120) NOT NULL,\nINSTANCE_NAME VARCHAR(200) NOT NULL,\nLAST_CHECKIN_TIME BIGINT(19) NOT NULL,\nCHECKIN_INTERVAL BIGINT(19) NOT NULL,\nPRIMARY KEY (SCHED_NAME,INSTANCE_NAME))\nENGINE=InnoDB;\n\nCREATE TABLE QRTZ_LOCKS (\nSCHED_NAME VARCHAR(120) NOT NULL,\nLOCK_NAME VARCHAR(40) NOT NULL,\nPRIMARY KEY (SCHED_NAME,LOCK_NAME))\nENGINE=InnoDB;\n\nCREATE INDEX IDX_QRTZ_J_REQ_RECOVERY ON QRTZ_JOB_DETAILS(SCHED_NAME,REQUESTS_RECOVERY);\nCREATE INDEX IDX_QRTZ_J_GRP ON QRTZ_JOB_DETAILS(SCHED_NAME,JOB_GROUP);\n\nCREATE INDEX IDX_QRTZ_T_J ON QRTZ_TRIGGERS(SCHED_NAME,JOB_NAME,JOB_GROUP);\nCREATE INDEX IDX_QRTZ_T_JG ON QRTZ_TRIGGERS(SCHED_NAME,JOB_GROUP);\nCREATE INDEX IDX_QRTZ_T_C ON QRTZ_TRIGGERS(SCHED_NAME,CALENDAR_NAME);\nCREATE INDEX IDX_QRTZ_T_G ON QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_GROUP);\nCREATE INDEX IDX_QRTZ_T_STATE ON QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_STATE);\nCREATE INDEX IDX_QRTZ_T_N_STATE ON QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP,TRIGGER_STATE);\nCREATE INDEX IDX_QRTZ_T_N_G_STATE ON QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_GROUP,TRIGGER_STATE);\nCREATE INDEX IDX_QRTZ_T_NEXT_FIRE_TIME ON QRTZ_TRIGGERS(SCHED_NAME,NEXT_FIRE_TIME);\nCREATE INDEX IDX_QRTZ_T_NFT_ST ON QRTZ_TRIGGERS(SCHED_NAME,TRIGGER_STATE,NEXT_FIRE_TIME);\nCREATE INDEX IDX_QRTZ_T_NFT_MISFIRE ON QRTZ_TRIGGERS(SCHED_NAME,MISFIRE_INSTR,NEXT_FIRE_TIME);\nCREATE INDEX IDX_QRTZ_T_NFT_ST_MISFIRE ON QRTZ_TRIGGERS(SCHED_NAME,MISFIRE_INSTR,NEXT_FIRE_TIME,TRIGGER_STATE);\nCREATE INDEX IDX_QRTZ_T_NFT_ST_MISFIRE_GRP ON QRTZ_TRIGGERS(SCHED_NAME,MISFIRE_INSTR,NEXT_FIRE_TIME,TRIGGER_GROUP,TRIGGER_STATE);\n\nCREATE INDEX IDX_QRTZ_FT_TRIG_INST_NAME ON QRTZ_FIRED_TRIGGERS(SCHED_NAME,INSTANCE_NAME);\nCREATE INDEX IDX_QRTZ_FT_INST_JOB_REQ_RCVRY ON QRTZ_FIRED_TRIGGERS(SCHED_NAME,INSTANCE_NAME,REQUESTS_RECOVERY);\nCREATE INDEX IDX_QRTZ_FT_J_G ON QRTZ_FIRED_TRIGGERS(SCHED_NAME,JOB_NAME,JOB_GROUP);\nCREATE INDEX IDX_QRTZ_FT_JG ON QRTZ_FIRED_TRIGGERS(SCHED_NAME,JOB_GROUP);\nCREATE INDEX IDX_QRTZ_FT_T_G ON QRTZ_FIRED_TRIGGERS(SCHED_NAME,TRIGGER_NAME,TRIGGER_GROUP);\nCREATE INDEX IDX_QRTZ_FT_TG ON QRTZ_FIRED_TRIGGERS(SCHED_NAME,TRIGGER_GROUP);\n\ncommit; "
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}