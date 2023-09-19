package com.worldpath.community.service;

import com.worldpath.community.model.Topic;
import com.worldpath.community.repositories.TopicRepository;
import org.springframework.stereotype.Service;

@Service
public class TopicService {
    private final TopicRepository topicRepository;

    public TopicService(TopicRepository topicRepository) {
        this.topicRepository = topicRepository;
    }

    public Topic createTopic(Topic topic) {
        // save to database
        return topicRepository.save(topic);
    }
}
