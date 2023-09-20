package com.worldpath.community.service;

import org.modelmapper.ModelMapper;
import org.springframework.stereotype.Service;
import com.worldpath.community.DTO.TopicDTO;
import com.worldpath.community.model.Topic;
import com.worldpath.community.repositories.TopicRepository;

@Service
public class TopicService {
    private final TopicRepository topicRepository;
    private final ModelMapper modelMapper;

    public TopicService(TopicRepository topicRepository, ModelMapper modelMapper) {
        this.topicRepository = topicRepository;
        this.modelMapper = modelMapper;
    }

    public TopicDTO getTopicById(String id) {
        Topic topic = topicRepository.findById(id).orElse(null);

        return modelMapper.map(topic, TopicDTO.class);
    }

    public TopicDTO createTopic(TopicDTO topicDTO) {
        Topic topic = modelMapper.map(topicDTO, Topic.class);

        // Save topic to database
        Topic savedTopic = topicRepository.save(topic);

        return modelMapper.map(savedTopic, TopicDTO.class);
    }
}
