package com.worldpath.community.repositories;

import org.springframework.data.mongodb.repository.MongoRepository;

import com.worldpath.community.model.Topic;

public interface TopicRepository extends MongoRepository<Topic, String> {}
