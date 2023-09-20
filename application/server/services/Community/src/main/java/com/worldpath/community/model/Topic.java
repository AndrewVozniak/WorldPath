package com.worldpath.community.model;

import lombok.Getter;
import lombok.Setter;

import org.springframework.data.mongodb.core.mapping.Document;
import javax.validation.constraints.NotEmpty;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Field;

import java.util.Date;

@Getter
@Setter
@Document(collection = "Topics")
public class Topic {
    @Id
    private String id;

    @NotEmpty(message = "Title is required.")
    private String title;

    @NotEmpty(message = "Description is required.")
    private String description;

    @NotEmpty(message = "User ID is required.")
    @Field("user_id")
    private String userId;

    @Field("updated_at")
    private Date updatedAt;

    @Field("created_at")
    private Date createdAt;

    public void createTopicDate() {
        this.updatedAt = new Date();
        this.createdAt = new Date();
    }

    public void updateTopic(String Title, String Description) {
        if (Title != null && !Title.isEmpty()) {
            this.title = Title;
        }

        if (Description != null && !Description.isEmpty()) {
            this.description = Description;
        }

        this.updatedAt = new Date();
    }
}
